# Sequence diagrams samples

##  SequenceDiagramSamples.IfElse()

**Root type:** `SequenceDiagramSamples` (from: `Synergy.Architecture.Tests`)

**Root method:**
```
    IfElse() : void
```

![Sequence Diagram for SequenceDiagramSamples.IfElse()](http://www.plantuml.com/plantuml/png/T99DKiCm38NtFeMcArWatMUOJilV30jMvG8YLM8FZXKijOQpiU18N06dWT8opSxol4zFVlBNn-TI5ADkEs_anOKU8tOGINeEGeyaWYrTa-UqYf4JQDoIQPYL8ZnP9x1ns0w8M4u2hYd82vL6dNeoQthTKgZfTXIjiUizIVdGt6NHnQMXi1c5M2j7M7D7g_q91MP31VjoPkOGiYNmZevK5UJo7Io63KY-D7buTRNhCIYSjpwHtDZudxftaHf-CmUFOd6-KGM7Hw1NI9O2jCoj9yYXu0wTn-V918h5w5s1LUsbcizJIkN8bpLtazZFVATSNiSlDuGFh9PYSZBjVhRDaNtUory4LqKn4UOowuQFymq00F__0m00)
<!--
@startuml
skinparam responseMessageBelowArrow true
footer This diagram shows if-else.
title
SequenceDiagramSamples.IfElse()
endtitle
actor SomeActor as "Some Actor"
/ note over SomeActor: very hand some
participant SequenceDiagramSamples
participant Chrome
participant Firefox
SomeActor->SequenceDiagramSamples: IfElse()
alt when google is available
SequenceDiagramSamples->Chrome: https://www.google.com
SequenceDiagramSamples->Firefox: https://www.foogle.com
else otherwise
SequenceDiagramSamples->Firefox: https://www.google.com
end
SomeActor<--SequenceDiagramSamples
@enduml
-->

This diagram shows if-else.


##  SequenceDiagramSamples.LoopAfterLoop()

**Root type:** `SequenceDiagramSamples` (from: `Synergy.Architecture.Tests`)

**Root method:**
```
    LoopAfterLoop() : void
```

![Sequence Diagram for SequenceDiagramSamples.LoopAfterLoop()](http://www.plantuml.com/plantuml/png/b591KiCm3Bpx5PfoWKFIUuRfjC1mWbFX0IPHOW-EPIo5mDiuy2I-W9t0j6NQ0oTBzgwqMldhutFDegCClLFyR7tGKVSGaGDvndjarXrUeQDn4oED8750rH89HdWmbg6nkiiKDZGoEA80mUaQ6z1j1cbFOjAPNqebLXogBRuCw6kycRbRtGU7NDubo2QJSd1-eT0tCrxNGX4U0wDifb0pBAOKfdm1Pn4RjOJK2u5USHzSGShVOSWNAeqdjhP1Uu7ZAWuWroPIZmTNjpPYIszgrw1O7QzKmT-19dToR7q7WnVhW5DzCJatEWJqVCASOZLhgS281AwMot4SouweSrZMr9-a_UWzvAK5_lAIoREkQPjfYuDhu0dJNjiM8wQ9y-l_ok_9okLtLbqMnGdNrmcO_-0t0000__y30000)
<!--
@startuml
skinparam responseMessageBelowArrow true
footer This diagram shows loop placed after another loop.
title
SequenceDiagramSamples.LoopAfterLoop()
endtitle
actor UpsetActor as "Upset Actor" #red
/ note over UpsetActor: very upset
participant SequenceDiagramSamples
participant Chrome
participant Firefox
UpsetActor->SequenceDiagramSamples: LoopAfterLoop()
loop Looping until something happens
SequenceDiagramSamples->Chrome: https://www.google.com
SequenceDiagramSamples->Firefox: https://www.foogle.com
end
loop This should be different loop
SequenceDiagramSamples->Firefox: https://www.google.com
end
UpsetActor<--SequenceDiagramSamples
@enduml
-->

This diagram shows loop placed after another loop.


##  SequenceDiagramSamples.Upsert()

**Root type:** `SequenceDiagramSamples` (from: `Synergy.Architecture.Tests`)

**Root method:**
```
    Upsert() : void
```

![Sequence Diagram for SequenceDiagramSamples.Upsert()](http://www.plantuml.com/plantuml/png/Z9BFJW8n4CRlVOe99pNPVG2Y19Grae0OTj63SXZf08sxxTePWZwR1n_9Lx05mPYi2PUsN_FDRxx--VhuxBAWbtLLAdunjaQF5NZYsbcc4J7ZaYwfT9kUzsu3ujUa5iu9UIXMXa4RNCOINhaDGs1PZLw3Hi5dP09NaqSnWPOgCLAIokbrJNPE_LrbZbLT4gVJcidBoQaYgtV6a4NCtDHe1N9N4Sx5UK26LbHFTgjRVssDR7L8q_zPg0ClwJJNjE4GgDcGTFQmDkJPCBigu0ok9-CHF0w4gXayt6IJB0WzWmlech0tq0fB0RE0g40xOh1EWDuC2xoJ7D5gS9jdao9CnNZVwButd6Ovf6cgg0o7ZCCHfEbTlrV4W5j8dXMHq9mwFCdlZPqdoJ_Nt0s--96-0G00__y30000)
<!--
@startuml
skinparam responseMessageBelowArrow true
footer This diagram shows standard database operations.
title
SequenceDiagramSamples.Upsert()
endtitle
participant Someactor as "Some\nactor"
participant SequenceDiagramSamples
database Database
Someactor->SequenceDiagramSamples: Upsert()
SequenceDiagramSamples->Database: SELECT * FROM [Item] WHERE [Id] = @itemId
alt if item does not exist yet
SequenceDiagramSamples->Database: INSERT INTO [Item] VALUES ...
else else
SequenceDiagramSamples->Database: UPDATE [Item] SET ... WHERE [Id] = @itemId
end
Someactor<--SequenceDiagramSamples
@enduml
-->

This diagram shows standard database operations.


##  SequenceDiagramSamples.OverrideMessageAndResult()

**Root type:** `SequenceDiagramSamples` (from: `Synergy.Architecture.Tests`)

**Root method:**
```
    OverrideMessageAndResult() : void
```

![Sequence Diagram for SequenceDiagramSamples.OverrideMessageAndResult()](http://www.plantuml.com/plantuml/png/V97FJiCm38VlUGfh9pYqcpXMQDhu8v0GciHs0mvHuxKHIHmSbufduy0ZyGgqwm21EYwHBEVx_7FoyVQ-IA9P6kzKUh8XQjOU65EaaF0MKz8Ldg6ZTibCBGWtgBP4WWoRsYOmLbSzacfg4tG7206z8BCr27uGW0wcbpPEO4iCnCO6pQzmlyRd1aE95uFcN3ltc2knub3zwQsrZmvJljhBz-6MmTpjp4V72eCPq9A2C3bOaqTTIZTI9vZqrKFOrHFLRIgsj547WV59lwvSeul8wbkOpSUX0lx9Duvayq5UmDNb1cgHc8hfj6tRl2Ag7EOb-GFeQPPziIUp6QnkVW9shGDhBRfdwl_x4m00__y30000)
<!--
@startuml
skinparam responseMessageBelowArrow true
footer This diagram shows how to override message and result for ordinary [SequenceDiagramCall].
title
SequenceDiagramSamples.OverrideMessageAndResult()
endtitle
control Someactor as "Some\nactor"
participant SequenceDiagramSamples
participant Helper
Someactor->SequenceDiagramSamples: OverrideMessageAndResult()
SequenceDiagramSamples->Helper: GET https://www.google.com
SequenceDiagramSamples<--Helper: 200 OK
Someactor<--SequenceDiagramSamples
@enduml
-->

This diagram shows how to override message and result for ordinary [SequenceDiagramCall].


