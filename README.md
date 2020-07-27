# SharpCraft
SharpCraft is a C# class library used for generating Minecraft datapacks. SharpCraft is made for making it easier to abstract things. SharpCraft in itself doesn't come with much abstraction, but allows people to make their own abstraction libraries which they then can reuse.

### Different way of writing
Generating a datapack in SharpCraft is not 100% the same as writing a normal datapack since all the syntax is different.
Example:

```mcfunction
#How you write commands in a function file in Minecraft
say hello
scoreboard objectives add TestScores dummy
scoreboard players set @a TestScores 1
tp @p[tag=test,distance=..4] ~1 ~3 5
summon creeper ^ ^ ^5 {ExplosionRadius:10,PersistenceRequired:1b}
```
```c#
//How you write commands in a function file in SharpCraft
MyFunction.World.Say("hello");
Objective testScores = MyFunction.World.Objective.Add("TestScores");
MyFunction.Entity.Score.Set(ID.Selector.a, testScores, 1);
MyFunction.Entity.Teleport(new Selector(ID.Selector.p, "test") { Distance = new MCRange(null, 4) }, new Coords(1, 3, 5, true, true, false));
MyFunction.Entity.Add(new LocalCoords(0, 0, 5), new Entities.Creeper() { ExplosionRadius = 10, PersistenceRequired = true });
```
The SharpCraft syntax is a quite a bit longer than the Minecraft Syntax. And that is a little bit of a problem, but with a good IDE you can use auto completion which helps a lot. Also tried to make the syntax shorter in some places eg: strings can implicit be converted into tags (Happens in the example tp command).
But why use SharpCraft if things takes a little longer to type sometimes? Well because you can mix it with C# to generate things like this:
```c#
//SharpCraft is used for generating
string[] words = stringFromSomewhereElse.Split(',');
for (int i = 0; i < words.length; i++) 
{
  MyFunction.Entity.Add(new Coords(), new Entities.Zombie() { CustomName = words[i] });
}
```
Not sold yet? Take a look at the abstraction library example.

### Documentation
[Hello world example]([Hello world example](https://github.com/Vilder50/SharpCraft/wiki/Hello-world-example))

Abstraction library example (Coming at some point)

Working with none SharpCraft datapacks (Coming at some point)

Data Documentation (Coming at some point)

Command Documentation (Coming at some point)

Files Documentation (Coming at some point)

Writers/Generators Documentation (Coming at some point)

### Why use SharpCraft?
I know not everyone will like the idea of using this. I do actually think there could be made a better alternative to SharpCraft. Something which would build ontop of the normal Minecraft syntax and allow everything SharpCraft allows people to do. [Trident](https://energyxxer.com/trident/#home) pretty much does this, but I still wanted to release this.

#### The positives:
* SharpCraft contains a good amount of information. Meaning you won't have to look at the wiki over and over to figure out things like what the syntax is for loot table function which sets the attribute on an item or which number is what dragon phase.

* SharpCraft allows you to generate files. Meaning you won't have to write your 30 nearly identical level selection functions + if you ever want to make a change to the functions then it's super simple since you just have to change how the files are generated.

* SharpCraft allows you to abstract things. Meaning you only have to write your right click detection system once and then you can reuse it over and over in different projects without having to think about all the function calling stuff since that's abstracted away.

* SharpCraft adds a small layer of abstraction ontop of Minecraft's syntax. Meaning that if Minecraf ever would change some syntax eg. combine /locate and /locatebiome into one command, you won't have to think about going through your files fixing it. SharpCraft will just change the command it outputs and then everything is fixed. (SharpCraft would ofcourse have to updated first to do it, but still...)

* SharpCraft is a C# library. Meaning you can use C# features like eg. read a file and generate a datapack based on whats inside of the file or you could make a thing in C# for drawing textures based on some information (like how rare a thing is) and also use that information for generating your datapack. Meaning textures are always up to date with your commands

* SharpCraft has a few abstraction things inbuilt like: Math operations with constants, easy to make search trees, easy way of placing structures, simple loops, random number generation and more. 

#### The negatives:
* Writing simple things takes longer.
* Syntax is different meaning it takes some time to get used to.
* Chance of things not being generated in the right way. Minecraft contains a lot of different nbt tags and such and there is a chance that some of them aren't added correctly to SharpCraft.
