using Design.ORiN3.Common.V1;
using Design.ORiN3.Provider.V1;
using Design.ORiN3.Provider.V1.Characteristic;

namespace Codeer.LowCode.Bindings.ORiN3.Server.TypeBranch
{
    internal class CreateVariableBranch(IChildCreator parent, string name, string type, string Option) : IValueTypeBranchAsync
    {
        private readonly IChildCreator _parent = parent;
        private readonly string _name = name;
        private readonly string _type = type;
        private readonly string _option = Option;

        public IVariable? Variable { get; private set; }

        public async Task CaseOfBoolAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<bool>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfBoolArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<bool[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableBoolAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<bool?>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableBoolArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<bool?[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfInt8Async(CancellationToken token) => Variable = await _parent.CreateVariableAsync<sbyte>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfInt8ArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<sbyte[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableInt8Async(CancellationToken token) => Variable = await _parent.CreateVariableAsync<sbyte?>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableInt8ArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<sbyte?[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfInt16Async(CancellationToken token) => Variable = await _parent.CreateVariableAsync<short>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfInt16ArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<short[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableInt16Async(CancellationToken token) => Variable = await _parent.CreateVariableAsync<short?>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableInt16ArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<short?[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfInt32Async(CancellationToken token) => Variable = await _parent.CreateVariableAsync<int>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfInt32ArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<int[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableInt32Async(CancellationToken token) => Variable = await _parent.CreateVariableAsync<int?>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableInt32ArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<int?[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfInt64Async(CancellationToken token) => Variable = await _parent.CreateVariableAsync<long>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfInt64ArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<long[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableInt64Async(CancellationToken token) => Variable = await _parent.CreateVariableAsync<long?>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableInt64ArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<long?[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfUInt8Async(CancellationToken token) => Variable = await _parent.CreateVariableAsync<byte>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfUInt8ArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<byte[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableUInt8Async(CancellationToken token) => Variable = await _parent.CreateVariableAsync<byte?>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableUInt8ArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<byte?[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfUInt16Async(CancellationToken token) => Variable = await _parent.CreateVariableAsync<ushort>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfUInt16ArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<ushort[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableUInt16Async(CancellationToken token) => Variable = await _parent.CreateVariableAsync<ushort?>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableUInt16ArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<ushort?[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfUInt32Async(CancellationToken token) => Variable = await _parent.CreateVariableAsync<uint>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfUInt32ArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<uint[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableUInt32Async(CancellationToken token) => Variable = await _parent.CreateVariableAsync<uint?>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableUInt32ArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<uint?[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfUInt64Async(CancellationToken token) => Variable = await _parent.CreateVariableAsync<ulong>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfUInt64ArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<ulong[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableUInt64Async(CancellationToken token) => Variable = await _parent.CreateVariableAsync<ulong?>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableUInt64ArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<ulong?[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfFloatAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<float>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfFloatArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<float[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableFloatAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<float?>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableFloatArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<float?[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfDoubleAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<double>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfDoubleArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<double[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableDoubleAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<double?>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableDoubleArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<double?[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfStringAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<string>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfStringArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<string[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfDateTimeAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<DateTime>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfDateTimeArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<DateTime[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableDateTimeAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<DateTime?>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfNullableDateTimeArrayAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<DateTime?[]>(_name, _type, _option, token).ConfigureAwait(false);
        public async Task CaseOfObjectAsync(CancellationToken token) => Variable = await _parent.CreateVariableAsync<object>(_name, _type, _option, token).ConfigureAwait(false);
        public Task CaseOfErrorAsync(CancellationToken token) => throw new Exception();
    }
}
