# iACodeChallenge

This is my first crack at it.<br>
All of the major requirements are met.<br>
Much refinement is needed.<br>

Assumptions:
1. A "central fill" location represents a coordinate pair. Currently, there can be only one "facility" per CentralFill coordinate pair.
2. Each facility has exactly three medications in its inventory:  A, B, and C.
3. A, B, and C medications have been assigned random monetary values between $0.01 and $100.00 USD.
4. By default, the coordinate system is -10 to 10 on X and Y coordinates. This yields 441 unique coordinate pairs or "locations".
5. When the program is run, a defined number (but no more than 441) CentralFill objects are created and assigned random coordinates.

To Do:
- Add a debug flag to view more info
- Add config file (possbily a simple JSON) to contain:
    - GridSize
    - FacilityCount
    - Medications
    - PriceMin
    - PriceMax