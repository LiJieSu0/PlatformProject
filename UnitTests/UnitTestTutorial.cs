using Godot;
using System;
using GdUnit4;

[TestSuite]
public partial class UnitTestTutorial : Node{
	[TestCase]
	public void TestPlus(){
		Assertions.AssertInt(2).Equals(1+1);
	}

}
