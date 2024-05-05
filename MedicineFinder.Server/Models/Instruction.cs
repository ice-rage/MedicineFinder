using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class Instruction
{
    [JsonPropertyName("storageCondition")]
    public string StorageCondition { get; set; }

    [JsonPropertyName("storageTime")]
    public string StorageLife { get; set; }

    [JsonPropertyName("companies")]
    public List<Company> Companies { get; set; }

    [JsonPropertyName("phInfluence")]
    public string PharmacologicalEffect { get; set; }

    [JsonPropertyName("phKinetics")]
    public string Pharmacokinetics { get; set; }

    [JsonPropertyName("dosage")]
    public string DosageRegimen { get; set; }

    [JsonPropertyName("overDosage")]
    public string Overdosage { get; set; }

    [JsonPropertyName("interaction")]
    public string Interaction { get; set; }

    [JsonPropertyName("lactation")]
    public string Lactation { get; set; }

    [JsonPropertyName("sideEffects")]
    public string SideEffects { get; set; }

    [JsonPropertyName("indication")]
    public string Indications { get; set; }

    [JsonPropertyName("contraIndication")]
    public string Contraindication { get; set; }

    [JsonPropertyName("specialInstruction")]
    public string SpecialInstructions { get; set; }

    [JsonPropertyName("renalInsuf")]
    public object RenalImpairedUsage { get; set; }

    [JsonPropertyName("hepatoInsuf")]
    public object HepaticImpairedUsage { get; set; }

    [JsonPropertyName("pharmDelivery")]
    public string ImplementationTerms { get; set; }

    [JsonPropertyName("elderlyInsuf")]
    public object ElderyUsage { get; set; }

    [JsonPropertyName("childInsuf")]
    public string ChildrenUsage { get; set; }
}