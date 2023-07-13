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


##  SequenceDiagramSamples.Database()

**Root type:** `SequenceDiagramSamples` (from: `Synergy.Architecture.Tests`)

**Root method:**
```
    Database() : void
```

![Sequence Diagram for SequenceDiagramSamples.Database()](http://www.plantuml.com/plantuml/png/Z5BDJW8n4BvlikymuQGckmz0bA2oHXAGX5trW1m6Ea1ZjrqxGz1dy-0Z-GfsaJN4GCAbpTTyVvts-_Ehou9UrgM98txLjaAF9NZYobcc8J7Zacx8kCsrzsu3ujSKHmldX3mKAysWD2vh3Q_SXY6uMOLUWKB16JA1gyYZw62Nnf5eCK6VqzkQx9nwlzeSoyeGfxsTvkmyZiYg7JbK4ZtN5Lg1t9M4St4UaA5LenUxnQr_l8C1SVJNgeaANHhBf7DOrORzNeSvIQVXj27F1jbj0HTmDnuDOT8NAgVmV9-Dim3K5AwWgyDPNyKH6W6zW1g2SiHWdG2zQnRu83abhF-GP-CYRCMeYNgw7ZncEQHfc3UPSDTwESchhZ4fS6Pe6cvHr1P7YeVNsPlSPP8S6tWtCBUVwmS00F__0m00)
<!--
@startuml
skinparam responseMessageBelowArrow true
footer This diagram shows standard database operations.
title
SequenceDiagramSamples.Database()
endtitle
participant Someactor as "Some\nactor"
participant SequenceDiagramSamples
database Database
Someactor->SequenceDiagramSamples: Database()
SequenceDiagramSamples->Database: SELECT * FROM [Item] WHERE [Id] = @itemId
alt if item does not exist yet
SequenceDiagramSamples->Database: INSERT INTO [Item] VALUES ...
else else
SequenceDiagramSamples->Database: Item [Table] SET ... WHERE [Id] = @itemId
end
Someactor<--SequenceDiagramSamples
@enduml
-->

This diagram shows standard database operations.


