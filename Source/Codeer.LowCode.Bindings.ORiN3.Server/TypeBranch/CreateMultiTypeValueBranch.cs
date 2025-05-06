using Codeer.LowCode.Blazor.Repository;
using Design.ORiN3.Common.V1;
using Design.ORiN3.Provider.V1;

namespace Codeer.LowCode.Bindings.ORiN3.Server.TypeBranch
{
    internal class CreateMultiTypeValueBranch(IVariable variable) : IValueTypeBranchAsync
    {
        private readonly IVariable _variable = variable;

        public MultiTypeValue Value { get; private set; } = MultiTypeValue.Create(null);

        public async Task CaseOfBoolAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<bool>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfBoolArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<bool[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableBoolAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<bool?>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableBoolArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<bool?[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfInt8Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<sbyte>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfInt8ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<sbyte[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableInt8Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<sbyte?>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableInt8ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<sbyte?[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfInt16Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<short>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfInt16ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<short[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableInt16Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<short?>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableInt16ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<short?[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfInt32Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<int>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfInt32ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<int[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableInt32Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<int?>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableInt32ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<int?[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfInt64Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<long>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfInt64ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<long[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableInt64Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<long?>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableInt64ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<long?[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfUInt8Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<byte>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfUInt8ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<byte[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableUInt8Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<byte?>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableUInt8ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<byte?[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfUInt16Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<ushort>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfUInt16ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<ushort[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableUInt16Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<ushort?>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableUInt16ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<ushort?[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfUInt32Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<uint>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfUInt32ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<uint[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableUInt32Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<uint?>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableUInt32ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<uint?[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfUInt64Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<ulong>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfUInt64ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<ulong[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableUInt64Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<ulong?>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableUInt64ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<ulong?[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfFloatAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<float>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfFloatArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<float[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableFloatAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<float?>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableFloatArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<float?[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfDoubleAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<double>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfDoubleArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<double[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableDoubleAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<double?>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableDoubleArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<double?[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfStringAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<string>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfStringArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<string[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfDateTimeAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<DateTime>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfDateTimeArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<DateTime[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableDateTimeAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<DateTime?>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfNullableDateTimeArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<DateTime?[]>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public async Task CaseOfObjectAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<object>)_variable).GetValueAsync(token).ConfigureAwait(false));
        public Task CaseOfErrorAsync(CancellationToken token) => throw new Exception();
    }
}
