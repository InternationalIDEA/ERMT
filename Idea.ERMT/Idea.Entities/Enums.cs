namespace Idea.Entities
{
    public enum ERMTControl
    {
        //add in alphabetic order.
        About,
        EditRegion,
        ElectoralCycle,
        ElectoralCycleModifyPhase,
        FactorNew,
        FactorModify,
        FactorReorder,
        KnowledgeResources,
        Login,
        MarkerTypeCRUD,
        ModelEdit,
        ModelNew,
        ModelReorderFactors,
        RiskActionRegister,
        RiskMapping,
        Start,
        TestUserControl,
        UserModify,
        UserNew,
        UserResetPassword
    }

    public enum RegionType
    {
        World,
        Continent,
        Country,
        Province,
        Administrative
    }

    public enum ShapeFileERMTType
    {
        Map,
        POI,
        Path
    }

    public enum AltitudeMode
    {
        ClampToGround = 0,
        RelativeToGround = 1,
        Absolute = 2
    }

    public enum LegendItemType
    {
        Scale,
        Cumulative,
        MarkerType
    }

    public enum ERMTDocumentType
    {
        Document,
        Icon,
        Shapefile,
        HTML,
        Backup
    }

    public enum RegionLevel
    {
        World = 0, Continent = 1, Country = 2, Province = 3, FirstAdmin = 4, SecondAdmin = 5, ThirdAdmin = 6, FourthAdmin = 7
    }
}
