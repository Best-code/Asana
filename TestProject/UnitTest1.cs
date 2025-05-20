using MyApp.Models;

namespace TestProject;

public class UnitTest1 
{
    [Fact]
    public void CreateProject_NameColin_DescriptionColinAPOSsSpaceStuff()
    {
        var fakeUi = new FakeUserInterface();
        fakeUi.AddInput("1");
        fakeUi.AddInput("Colin");
        fakeUi.AddInput("Colin's Stuff");

        var unit = new AsanaUnit(fakeUi);
        unit.Run();

        Assert.Single(unit.Projects);
        Assert.Equal("Colin", unit.Projects[0].Name);
        Assert.Equal("Colin's Stuff", unit.Projects[0].Description);
        Assert.Single(unit.Projects);
    }
}
