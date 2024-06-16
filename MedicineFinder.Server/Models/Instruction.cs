#pragma warning disable CS0659

using System.Text.Json;
using System.Text.Json.Serialization;

namespace MedicineFinder.Server.Models;

public class Instruction : ICloneable
{
    [JsonPropertyName("storageCondition")]
    public string StorageCondition { get; set; }

    [JsonPropertyName("storageTime")]
    public string StorageLife { get; set; }

    [JsonPropertyName("companies")]
    public List<CompanyDetailed> Companies { get; set; }

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
    public string Contraindications { get; set; }

    [JsonPropertyName("specialInstruction")]
    public string SpecialInstructions { get; set; }

    [JsonPropertyName("renalInsuf")]
    public string RenalImpairedUsage { get; set; }

    [JsonPropertyName("hepatoInsuf")]
    public string HepaticImpairedUsage { get; set; }

    [JsonPropertyName("pharmDelivery")]
    public string ImplementationTerms { get; set; }

    [JsonPropertyName("elderlyInsuf")]
    public string ElderlyUsage { get; set; }

    [JsonPropertyName("childInsuf")]
    public string ChildrenUsage { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        if (obj is not Instruction other)
        {
            return false;
        }

        return StorageCondition == other.StorageCondition &&
               StorageLife == other.StorageLife &&
               Companies.SequenceEqual(other.Companies) &&
               PharmacologicalEffect == other.PharmacologicalEffect &&
               Pharmacokinetics == other.Pharmacokinetics &&
               DosageRegimen == other.DosageRegimen &&
               Overdosage == other.Overdosage &&
               Interaction == other.Interaction &&
               Lactation == other.Lactation &&
               SideEffects == other.SideEffects &&
               Indications == other.Indications &&
               Contraindications == other.Contraindications &&
               SpecialInstructions == other.SpecialInstructions &&
               RenalImpairedUsage == other.RenalImpairedUsage &&
               HepaticImpairedUsage == other.HepaticImpairedUsage &&
               ImplementationTerms == other.ImplementationTerms &&
               ElderlyUsage == other.ElderlyUsage &&
               ChildrenUsage == other.ChildrenUsage;
    }

    /// <inheritdoc/>
    public object Clone()
    {
        var serialized = JsonSerializer.Serialize(this);

        return JsonSerializer.Deserialize<Instruction>(serialized);
    }
}