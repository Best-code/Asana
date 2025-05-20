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
        Assert.Contains("Project 'Colin' created\n", fakeUi.Outputs);

    }

    [Fact]
    public void CreateProject_DeleteProject_ProjectCountIs0()
    {
        var fakeUi = new FakeUserInterface();
        fakeUi.AddInput("1");
        fakeUi.AddInput("Colin");
        fakeUi.AddInput("Colin's Stuff");
        fakeUi.AddInput("2");
        fakeUi.AddInput("1");


        var unit = new AsanaUnit(fakeUi);
        unit.Run();

        Assert.Empty(unit.Projects);

    }

    [Fact]
    public void TryCreateProject_NoName_SetDefaultName()
    {
        var fakeUi = new FakeUserInterface();
        fakeUi.AddInput("1");
        fakeUi.AddInput(null);
        fakeUi.AddInput("");

        var unit = new AsanaUnit(fakeUi);
        unit.Run();

        Assert.Equal("Project", unit.Projects[0].Name);
    }

    [Fact]
    public void TryCreateProject_NoName_NoDescription_SetDefaultName_DescriptionCanBeNull()
    {
        var fakeUi = new FakeUserInterface();
        fakeUi.AddInput("1");
        fakeUi.AddInput(null);
        fakeUi.AddInput(null);

        var unit = new AsanaUnit(fakeUi);
        unit.Run();

        Assert.Equal("Project", unit.Projects[0].Name);
        Assert.Null(unit.Projects[0].Description);
    }

    [Fact]
    public void CreateProjectWithId1()
    {
        var fakeUi = new FakeUserInterface();
        fakeUi.AddInput("1");
        fakeUi.AddInput("Test name");
        fakeUi.AddInput("Test Description");

        var unit = new AsanaUnit(fakeUi);
        unit.Run();

        Assert.Single(unit.Projects);
        Assert.Equal(1, unit.Projects[0].Id);
    }

    [Fact]
    public void Create2ProjectsWithId1And2()
    {
        var fakeUi = new FakeUserInterface();
        fakeUi.AddInput("1");
        fakeUi.AddInput("Test name");
        fakeUi.AddInput("Test Description");

        fakeUi.AddInput("1");
        fakeUi.AddInput("Test name 2");
        fakeUi.AddInput("Test Description 2");


        var unit = new AsanaUnit(fakeUi);
        unit.Run();

        Assert.Equal(2, unit.Projects.Count());
        Assert.Equal(1, unit.Projects[0].Id);
        Assert.Equal(2, unit.Projects[1].Id);
    }

    [Fact]
    public void Create2ProjectsWithId1And2_DeleteProject1_CreateAnotherProjectWithId3()
    {
        var fakeUi = new FakeUserInterface();
        fakeUi.AddInput("1");
        fakeUi.AddInput("Test name");
        fakeUi.AddInput("Test Description");

        fakeUi.AddInput("1");
        fakeUi.AddInput("Test name 2");
        fakeUi.AddInput("Test Description 2");

        fakeUi.AddInput("2");
        fakeUi.AddInput("1");

        fakeUi.AddInput("1");
        fakeUi.AddInput("Test name 3");
        fakeUi.AddInput("Test Description 3");

        var unit = new AsanaUnit(fakeUi);
        unit.Run();

        Assert.Equal(2, unit.Projects.Count());
        Assert.Equal(2, unit.Projects[0].Id);
        Assert.Equal(3, unit.Projects[1].Id);
    }
}
