# Sequence diagrams samples

##  SequenceDiagramSamples.IfElse()

**Root type:** `SequenceDiagramSamples` (from: `Synergy.Architecture.Tests`)

**Root method:**
```
    IfElse() : void
```

![Sequence Diagram for SequenceDiagramSamples.IfElse()](http://www.plantuml.com/plantuml/png/V5BBJiD03Bn7oZzOFS4XwJr2LSjBui0f_819dUoApJgidIvy6mS-YLzW-m1QqVQsNiruncD_VNpEHJ7esBiyanVh1mpOGo0Ps0izaWXsT4sEuo84Zg1Xf3nhcPK2F1ahiBJOhJbYE0hOjY0dLEQPMdK9MjFhIBwXsosindvm9EL3UvTW5vTvHdwvWsAZ7A3cdXQR5mfCrXLioacUJS4dNU0LxS4gIEKx6FHBaFIPPsa6jOqTq2iSbp_4t9ZmZtTl0xNyblp_w1Ipuxqg-9i5dK8qvA5ZxXn1YWTNQ1q-duwYc6trAp2gWrJJQOont3Oe6-vFytOU3ubfDR_4zIQ0rL28LiuOEDfetq7QqLuILqLnAjPvGcvkwHi00F__0m00)
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

![Sequence Diagram for SequenceDiagramSamples.LoopAfterLoop()](http://www.plantuml.com/plantuml/png/b591JiCm4BnRoXzMvGA7fFS8LIqWJd0gFC0aczZ2yHfxG-1j77WINy1sAzgAzj1JlFRCpknEVhw-bu6bvt4mXGYlsZhfvG0UWoCRy153a3tUeA5fvJrDm7x4GdH4Z1wUb0xGQjadJb0q1J143foH3ROWkmIIbbZ5Rtgf2i6QJUomnhSHRODt6_PQ3ivWg1uYQ9LewN1vLGYqxPOX6oOFpouWh_9H1fZb4d8zWmkFRI7c40KHw1ttqJN4-XF6T568E2NhHZjf6OuxESJSAay37jxTQuyTVHHY9r8kZZUhuTzSUK-fqBQ7qR8s4A84grGhwHpQwE5uktAnyLE3OdQXdi-dQQfwejvWrT1mchVrV4YCOVuHuxgttdAsCTFHjF22CUMkGuznxlHwfiA-jQomMzbrMPuAO1cX-R_y1G00__y30000)
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

![Sequence Diagram for SequenceDiagramSamples.Upsert()](http://www.plantuml.com/plantuml/png/Z9B1JW8n48Rlc-mxJ3YfoUu342MWh944nD053yfXf0CqxhPhPmZwR1vy95_1Be8Q0mcNjd_ppzU_qtw-VxHPqCkgBEA8dusjq6C9dhXobcb0pBYWIohSkkEzMuFu5SNHt3aX3_dIC6Y3Yxg6bsxD46XMezUWKV09cS1Lv55CmALn94QAKA_ePKLsHjtlMeLbLH2duuh9oybf797LMsi896PcAhG2ofM4Ct4UaA5HgqUxqOr_lhtuEFh9rDqkGf8TCcdjhsh2RwhzZgIrmpL1PVtiAeSpk1uD1_3G4ogdS7-JZR8Wz1Gke6t2Na_74HO2PWwr1Es8mJe1UZKiy4Pop66zMvMDyZ3bmzrHaqv_d2b8qz1hAi9Dw-4OrlYksydha1kColAQSI1vU9m_ZJjFaaFzRWVdvcTz0G00__y30000)
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

![Sequence Diagram for SequenceDiagramSamples.OverrideMessageAndResult()](http://www.plantuml.com/plantuml/png/V97FIiD048VlWRp3q9CUaXQFGKhh7nH42gOtzR0aqsHninDd9ou-cmSVoLTmaxIWadfP66Q_Rtwp-_NnkNAYMBh6n95xqRPLh1fWT2rPX_VedAhm0WtvDJDv4EumZdP4WWpRMZiejQfwnjNa7OG3X83Ua5cN2Cre06NBtjePWHqn49VQAdw7nnnVExG5NesQIsNCSnf7eiM4GN-wkMfQWoxTxFNxV6jRFWpkazCuGblkuOAiC1d8gK5LI1Yh7CpwwiaEzIoEmhwY2zqgAp0zxFUTMpGjSWXsS2RBQIo3_q9ECybor6TmSxs5MgHrsNpklKyheifWMb1pZ3rFaWDyjbZ0vkv7nj0xjjigFDVm-Ty0003__mC0)
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


