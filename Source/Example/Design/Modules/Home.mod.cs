MemoryStream _onImage;
MemoryStream _offImage;
void DetailLayoutDesign_OnBeforeInitialization()
{
    _onImage = Resources.GetMemoryStream("on.png");
    _offImage = Resources.GetMemoryStream("off.png");
}

void R1Field_OnDataChanged()
{
    if (R1Field.Value)R1Image.SetMemoryStream("on.png", _onImage);
    else R1Image.SetMemoryStream("off.png", _offImage);
}