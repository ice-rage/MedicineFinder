namespace MedicineFinder.Server.Models
{
    public class AtcCode
    {
        public string code { get; set; }
        public string rusName { get; set; }
        public string engName { get; set; }
        public string parentCode { get; set; }
    }

    public class Children
    {
        public int id { get; set; }
        public string zipInfo { get; set; }
        public string composition { get; set; }
        public List<Company> companies { get; set; }
    }

    public class ClPhGroup
    {
        public string name { get; set; }
    }

    public class Company
    {
        public bool isRegistrationCertificate { get; set; }
        public bool isManufacturer { get; set; }
        public Company2 company { get; set; }
        public string companyRusNote { get; set; }
        public string rusName { get; set; }
        public string engName { get; set; }
        public string rusAddress { get; set; }
        public string shortAddress { get; set; }
        public Country country { get; set; }
    }

    public class Company2
    {
        public string name { get; set; }
        public string GDDBName { get; set; }
        public Country country { get; set; }
    }

    public class Country
    {
        public string code { get; set; }
        public string rusName { get; set; }
    }

    public class Document
    {
        public string storageCondition { get; set; }
        public string storageTime { get; set; }
        public List<Company> companies { get; set; }
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
        public object renalInsuf { get; set; }
        public object hepatoInsuf { get; set; }
        public string pharmDelivery { get; set; }
        public object elderlyInsuf { get; set; }
        public string childInsuf { get; set; }
    }

    public class GNParent
    {
        public string gnParent { get; set; }
        public string description { get; set; }
    }

    public class Molecule
    {
        public string latName { get; set; }
        public string rusName { get; set; }
        public GNParent GNParent { get; set; }
    }

    public class MoleculeName
    {
        public int id { get; set; }
        public Molecule molecule { get; set; }
    }

    public class Phthgroup
    {
        public string code { get; set; }
    }

    public class Product
    {
        public List<AtcCode> atcCodes { get; set; }
        public List<Phthgroup> phthgroups { get; set; }
        public List<ClPhGroup> ClPhGroups { get; set; }
        public List<string> images { get; set; }
        public List<MoleculeName> moleculeNames { get; set; }
        public string rusName { get; set; }
        public string engName { get; set; }
        public string zipInfo { get; set; }
        public string composition { get; set; }
        public List<Company> companies { get; set; }
        public Document document { get; set; }
        public List<Children> childrens { get; set; }
    }

    public class Root
    {
        public List<Product> products { get; set; }
    }
}