using System.Text.Json.Nodes;

namespace Codeer.LowCode.Bindings.ORiN3.Server
{
    internal class O3TreeSetting
    {
        internal enum ObjectType
        {
            RemoteEngine = 0,
            Root = 1,
            Controller = 2,
            Module = 3,
            Variable = 4,
            Event = 5,
            File = 6,
            Job = 7,
            Stream = 8,
        }

        internal record TreeObject(Guid Id, ObjectType ObjectType, IList<TreeObject> Children);

        public IList<TreeObject> Objects { get; } = [];

        public O3TreeSetting(JsonNode json)
        {
            foreach (var it in json["Objects"]!.AsArray())
            {
                Objects.Add(CreateTreeObject(it!));
            }
        }

        private static TreeObject CreateTreeObject(JsonNode json)
        {
            var id = json["Id"]!.GetValue<string>();
            var objectType = json["ObjectType"]!.GetValue<int>();
            var children = new List<TreeObject>();
            foreach (var child in json["Children"]!.AsArray())
            {
                children.Add(CreateTreeObject(child!));
            }
            return new TreeObject(Guid.Parse(id), (ObjectType)objectType, children);
        }
    }
}
