namespace MyTiendita.Shared.DTOs
{
    public record ProductDTO(
        string Barcode,
        string Description,
        Byte[]? Capture
        );
}