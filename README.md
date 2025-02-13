# iACodeChallenge - Central Fill Facility Grid

## Compiling and Running

This is a .NET console appliation written in C# to meet the requirements of iA Coding Challenge 2025.pdf.

#### Open the CentralFill.sln in Visual Studio.
* There is only one Nuget dependency - Newtonsoft.json - which helps read the config file, and this should load automatically.
* You can adjust config.json in the solution manager, and when the solution is built, config.json gets auto-copied to the bin
  folder when it is run.
* Activate DebugMode in config.json to see where the Facilities are on the grid, making testing easier.

#### Alternatively, you can find CentralFill.exe in bin/Debug/net8.0 and simply run that.
* config.json should be in the same folder as the exe.
* I woudln't normally be pushing built binary files to a git repo, but this one is quite small.

- - - -

## Assumptions
1. A "location" represents a coordinate pair. Currently, there can be only one "facility" per coordinate pair.
1. A "Central Fill Facility", a "Central Fill", and a "Facility" are logically the same thing.
   They are referred to "Facilities" in this appliction.
1. A Facility contains a set of coordinates that define its location.
1. A Facility contains a list of medications in its inventory.
1. By default, eaach facility has exactly three medications in its inventory: A, B, and C, but this is modifiable via the config file.
1. By default, the medications have been assigned random monetary values between $0.01 and $100.00 USD.
1. By default, the coordinate system is -10 to 10 on X and Y coordinates. This yields 441 unique coordinate pairs or "locations".
1. When the program is run, a defined (in config.json) number of Facility objects are created and assigned random coordinates.
1. If the number of facilities to be created exceeds the number of positions on the grid, facilities will be created until the grid is full.
1. It is possible for fewer than 3 facilities to be returned as the "3 closest" if there are fewer than 3 facilities were seeded on the grid.
1. If 0 facilities are seeded, 0 facilities will be returned in the search.

- - - -

## Configuration File: config.json
* Debug Mode: prints a list of seeded Facilities before asking for user input.
* GridSize: the size of the grid. 10 means the grid spans from -10 to 10 on the x and y axis and has 441 total locations available.
* FacilityCount: the number of Facilities to populate the grid with on the initial load.
* Medications: the medications that each Facility will be created with.
* PriceMin: the minimum value that a medication can be randomly generated to have.
* PriceMax: the maximum value that a medication can be randomly generated to have.

<pre>
{
  "DebugMode": false,
  "GridSize": 10,
  "FacilityCount": 10,
  "Medications": [ "A", "B", "C" ],
  "PriceMin": 0.01,
  "PriceMax": 100.0
}
</pre>

- - - -

## How I Might Change the Program If ...
### The program needed to support multiple facilities at the same location?
It can already handle them. Currently, I'm ensuring one facility per location by keeping track of my used coordinates.
Removing this check will allow multiple facilities to have the same coordiantes.
<pre>
do
{
    x = random.Next(-config.GridSize, config.GridSize + 1);
    y = random.Next(-config.GridSize, config.GridSize + 1);
} while (usedCoords.Contains((x, y)));
</pre>

### I had to work with a much larger world (grid) size?
FindClosestFacilities() is currently using a linear search O(n log n) to sort and thus find the closest facilities by Mahnattan Distance.
This is fine when there are only 441 possible facilities.<BR>
<BR>
If I were dealing with "millions" of facilities, performance would start to become a problem. I would implement a more complicated,
but more efficient searching algorithm. Something like a QuadTree, which would reduce lookup speed to O(log n) by dividing the grid
into partitions.