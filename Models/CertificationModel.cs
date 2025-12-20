public class CertificationModel
{
    public string OriginalCertificate { get; set; } = string.Empty; // The name to find for updating
    public string Certificate { get; set; } = string.Empty;   // The new name to set
    public string From { get; set; } = string.Empty;          // The new 'From' value
    public string Year { get; set; } = string.Empty;          // The new 'Year' value
}