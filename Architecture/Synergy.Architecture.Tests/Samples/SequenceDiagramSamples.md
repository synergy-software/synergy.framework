# Sequence diagrams samples

##  SequenceDiagramSamples.IfElse()

**Root type:** `SequenceDiagramSamples` (from: `Synergy.Architecture.Tests`)

**Root method:**
```
    IfElse() : void
```

![Sequence Diagram for SequenceDiagramSamples.IfElse()](http://www.plantuml.com/plantuml/png/T99BJiGm38RtEOKrArYqtLTeDCDBOi6gNC2qRXEHnYNsJE1iB3WILq2TedcWTXTB_-Sl-PMVh--DA4RTzTx8cmi3HkmXaWmSX9v913kw9Szf6oCdqBWZqp8hHNYnJg1ns8s8M4u2hit82-L6dNeo5RtlADHqFuKgx0TFaZ-r3rxewjfGQAOWrieHAkvfUpYXm6gSu32kJ061bO3tT9OgOK_n4oo61eHxCWD6TRKRC2ZCgoyYTpR-fnvTf9O_pD6HhUSNbN1y17g5P2b0nznv0YU0UtGUNnSRoDQJkmIhEaXP52cbVEBpclj5xEz-brnxuiRoWTLIJ4wMxRDhplGKcbC5DrcsKEQ6GZD-d5y0003__mC0)
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

![Sequence Diagram for SequenceDiagramSamples.LoopAfterLoop()](http://www.plantuml.com/plantuml/png/b591JiCm4Bpx5QjoWKFIUuIg5X0dE1KUOE9DRE5uZNVJmDiuy2I-WEmWjKNjWTliUcOzdlNtvzUQHISP1w_urOMeanuW8KSAZ8_8h7kyGK_J9YMQGDA8gYCIJF1a7ODnkiyIjZGnUA88qUiM3UWkatGWiPZAIQt4YKUrnRSHGujtitQhX-YHwmUYkCcY32wl50OpytKhbE0vCigcGCsmA2MKUW4N2OrQGY11e1qUaXlOOVg0CJTKr4bSww8E0gTT755kRQ81ZrhtBc57xsf_GRKwFQc1lmygwMJiGWzZ4EU1QK2nkROwHWny9fngDNjfm8f4RfRBQPhgdgZtMBSqd9Nz-ZtMTNiT1ZFxAjjaIwCty89WNDTXmY1bS_yRVs0hZzz7TLrLPr9VOp3v3_u0003__mC0)
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

![Sequence Diagram for SequenceDiagramSamples.Upsert()](http://www.plantuml.com/plantuml/png/Z99DIWD148NtVOeYAnLc3X0q99eM1_8ZcOakOXPbkf8qpdItNHMYPtFXaRo2J68YmWZPDFtWlQyUHNrzV7PPCCgcAXM_M1SmOWMHE7Z7D2HcND4rbNxRYz5lGUA6rD9xeGZ5sZ8OYwiwmck_PM11Pp0QC2ZuZ4pW0qKKwnsdIgoKf79wtP1RK7-VpB4A9N4w3KnHpiuLERCt1enY5pQW4yXzHRWG7m4PMhLwSZlT-cjhPAjZc_xFHnrvIQSvquPZeMP3qZd0sf3hWRufu09k9-CXp3AXQWwFTtgYOPQPELn1rmfLcL5O2jWbr0gC9mRd1UZDii0xoGcZib6k9mLaes9y6FJG6qnr3ccQAYgPe7vE84tl-xsYBhY3vBge2SsjoPdVZLqcoJzhxf8pzI5z0m00__y30000)
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

![Sequence Diagram for SequenceDiagramSamples.OverrideMessageAndResult()](http://www.plantuml.com/plantuml/png/V97FIWD138VlynIX9ptijdXSIcdzWu98mVQc7i9EkZiuEnapMHUVpOEFv2l8TgjYQJs6vFlo2_by_vWdHT6syIOzkn1Hi06X53aakgEKiA9pyjmjHBW3bPRCXbb9O5sx1DPXrIEfvYv1tOymy2k9E4lG30B0O7jfwnKsBC1YNK1vWuSLlRGKIheSD1Ve_LDkrAadizTROHCzfNova-_2BOAztvfFJWq5Ew0b1nNsiEA6i5GMm0IZldeCstfa8egwqaKC2eSt_Hcv8Hz9p8ymcns62lWdts4acmto0gwlrb2hnbICnrtNvHLpvIalkJc2JhFicpsRJ61v-njmccL7pffJiFs_lm000F__0m00)
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


