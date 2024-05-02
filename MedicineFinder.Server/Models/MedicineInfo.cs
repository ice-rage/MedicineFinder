namespace MedicineFinder.Server.Models
{
    public class Rootobject
    {
        public bool success { get; set; }
        public Product[] products { get; set; }
    }

    public class Product
    {
        public Atccode[] atcCodes { get; set; }
        public Phthgroup[] phthgroups { get; set; }
        public Clphgroup[] ClPhGroups { get; set; }
        public Moleculename[] moleculeNames { get; set; }
        public int id { get; set; }
        public string rusName { get; set; }
        public string engName { get; set; }
        public string zipInfo { get; set; }
        public string composition { get; set; }
        public Company1[] companies { get; set; }
        public Document document { get; set; }
    }

    public class Document
    {
        public string storageCondition { get; set; }
        public string storageTime { get; set; }
        public Company[] companies { get; set; }
        public int documentId { get; set; }
        public int articleId { get; set; }
        public string phInfluence { get; set; }
        public string phKinetics { get; set; }
        public string dosage { get; set; }
        public string overDosage { get; set; }
        public string interaction { get; set; }
        public string lactation { get; set; }
        public string sideEffects { get; set; }
        public string indication { get; set; }
        public string contraIndication { get; set; }
        public string specialInstruction { get; set; }
        public string renalInsuf { get; set; }
        public string hepatoInsuf { get; set; }
        public string pharmDelivery { get; set; }
        public string elderlyInsuf { get; set; }
        public string childInsuf { get; set; }
    }

    public class Company
    {
        public string rusName { get; set; }
        public string engName { get; set; }
        public string shortAddress { get; set; }
        public Country country { get; set; }
    }

    public class Country
    {
        public string rusName { get; set; }
    }

    public class Atccode
    {
        public string code { get; set; }
        public string rusName { get; set; }
        public string engName { get; set; }
    }

    public class Phthgroup
    {
        public string code { get; set; }
    }

    public class Clphgroup
    {
        public string name { get; set; }
    }

    public class Moleculename
    {
        public int id { get; set; }
        public Molecule molecule { get; set; }
        public string rusName { get; set; }
        public string engName { get; set; }
    }

    public class Molecule
    {
        public int id { get; set; }
        public string latName { get; set; }
        public string rusName { get; set; }
        public Gnparent GNParent { get; set; }
    }

    public class Gnparent
    {
        public string GNParent { get; set; }
        public string description { get; set; }
    }

    public class Company1
    {
        public bool isRegistrationCertificate { get; set; }
        public bool isManufacturer { get; set; }
        public Company2 company { get; set; }
    }

    public class Company2
    {
        public string name { get; set; }
        public string GDDBName { get; set; }
        public Country country { get; set; }
    }
}