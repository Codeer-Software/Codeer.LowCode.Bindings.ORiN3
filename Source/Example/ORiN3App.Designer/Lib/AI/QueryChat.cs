using System.ClientModel;
using System.Text.RegularExpressions;
using Azure.AI.OpenAI;
using Codeer.LowCode.Blazor.DataIO.Db.Definition;
using Codeer.LowCode.Blazor.Designer.Extensibility;
using Codeer.LowCode.Blazor.Designer.Extra;
using Codeer.LowCode.Blazor.DesignLogic;
using Codeer.LowCode.Blazor.Json;
using ControlzEx.Standard;
using OpenAI.Chat;
using static Codeer.LowCode.Blazor.Designer.Extra.QuerySettingPropertyControl;

namespace ORiN3App.Designer.Lib.AI
{
    internal class QueryChat : IQueryAIChat
    {
        List<ChatMessage> _chatHistory = new();
        AzureOpenAIClient _azureClient;
        ChatClient _chatClient;

        public event EventHandler<QueryAIChatEventArgs> DetermineSql = (_, __) => { };

        public string Module { get; set; } = string.Empty;

        public QueryChat(DesignerEnvironment designerEnvironment, AISettings settings, string dataSourceName)
        {
            _azureClient = new AzureOpenAIClient(
                new Uri(settings.OpenAIEndPoint),
                new ApiKeyCredential(settings.OpenAIKey));

            _chatClient = _azureClient.GetChatClient(settings.ChatModel);

            var info = designerEnvironment.GetDbInfo(dataSourceName);
            var dataSource = designerEnvironment.GetDesignerSettings().DataSources.FirstOrDefault(x => x.Name == dataSourceName);

            var dbType = dataSource?.DataSourceType.ToString() ?? string.Empty;
            _chatHistory.Add(new SystemChatMessage($@"
あなたはSQLの専門家です。
ユーザーが求めるSQLを書いてください。
ユーザーの使っているDBの種類は 
{dbType} です。

SQLの部分は以下のように囲ってください

```sql

```
このSQLはC#から実行します。パラメータをユーザーに求められた場合はその前提で書いてください。
またパラメータは指定しない場合はその条件を無視するようなSQLにしてください。
例えば以下のようなものです。
saledate >= @p1 OR @p1 IS NULL

最終的にSelectされる項目とパラメータは以下の形でも出力してください。
カラムはIsParameterを出力しないでください。
パラメータはIsParameterをtrueにしてNameは@や:などを含めた形で書いてください。
```schema
[
  {{
    ""Name"": ""name"",
    ""DbType"": ""db raw type""
  }},
  {{
    ""IsParameter"": true,
    ""Name"": ""@name"",
    ""DbType"": ""db raw type""
  }}
]
```

C#の解説は必要ありません。
SQLの解説も基本的には必要ありません。
求められたらトークン数の都合で説明は省いている旨のコメントを返してください。
簡潔にシンプルに迅速にSQLとスキーマを返してください。

たまにあなたが作ったSQLを実行すると失敗するときがあります。
その場青はユーザーはあなたにその旨を伝え作り直しを要求します。
その場合でも言い訳は書かなくていいので簡潔にシンプルに迅速にSQLとスキーマを返してください。


また既存のDBのテーブルでは実現不可能な場合はその旨をユーザーに伝えてください。
"));

            _chatHistory.Add(new SystemChatMessage($@"
現在のテーブル情報です。
{CreateDbInfo(info)}
"));
        }

        static string CreateDbInfo(List<DbTableDefinition> info)
        {
            var tables = new List<string>();
            foreach (var table in info)
            {
                var columns = new List<string>();
                foreach (var column in table.Columns)
                {
                    columns.Add($"{column.Name}:{column.RawDbTypeName}");
                }

                tables.Add($"{table.Name}:{{{string.Join(",", columns)}}}");
            }
            return string.Join("\n", tables);
        }

        public async Task<string> ProcessMessage(string userMessage)
        {
            _chatHistory.Add(new UserChatMessage(userMessage));
            var result = await _chatClient.CompleteChatAsync(_chatHistory);
            var resultText = result.Value.Content.FirstOrDefault()?.Text ?? string.Empty;
            _chatHistory.Add(new AssistantChatMessage(resultText));

            var matchSql = Regex.Match(resultText, @"```sql\s(.*?)\s```", RegexOptions.Singleline);
            var sql = string.Empty;
            if (matchSql.Success)
            {
                sql = matchSql.Groups[1].Value;
            }

            var dbParams = new List<DbParameterSetting>();
            var matchParams = Regex.Match(resultText, @"```schema\s*(\[.*?\])\s*```", RegexOptions.Singleline);
            if (matchParams.Success)
            {
                dbParams = JsonConverterEx.DeserializeObject<List<DbParameterSetting>>(matchParams.Groups[1].Value) ?? new();
            }

            if (!string.IsNullOrEmpty(sql))
            {
                DetermineSql(this, new QueryAIChatEventArgs { Sql = sql, Params = dbParams });
                return "作成しました、ご確認お願いします。";
            }

            return resultText;
        }
    }
}
