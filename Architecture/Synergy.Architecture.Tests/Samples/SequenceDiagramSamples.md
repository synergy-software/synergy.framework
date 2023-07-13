# Sequence diagrams samples

##  SequenceDiagramSamples.IfElse()

**Root type:** `SequenceDiagramSamples` (from: `Synergy.Architecture.Tests`)

**Root method:**
```
    IfElse() : void
```

![Sequence Diagram for SequenceDiagramSamples.IfElse()](http://www.plantuml.com/plantuml/png/V9BFJiCm3CRlbVeEravmQ7Ulq3J-IXmuZJiodTj4f769lGMUZGEFn2kGhjFOnBPR7Fq-v_Fd_7nzpqKnwAfpUIPlrlSOi8D0qhCNUY8HREc67CVh43Y2XXNbMSEi5E3PM86bnNRGYE4eO9k2d52PPshL9NH1xoloDTsDs0Axtf6KZyrzmYukyupySejYhHnWmHszZ4SKc0mbRCf9dat1fuU1rxJFLP3gJp3ebo3fDi_I56fhswDNE6xWaBaruP_kmGPg-2DDi7kec1rlLi7VEEWKeY4FBNFh2592k4RhyFLq6iLiD521KUsbcavZZENOeAovEwtRcZmKfktiXCCoWDLGY5RE63ZQQDz1MjD-55T5SIhOUK8t_-aN003__mC0)
<!--
@startuml
skinparam responseMessageBelowArrow true
footer This diagram shows if-else.
title
SequenceDiagramSamples.IfElse()
endtitle
actor Some_actor as "Some actor"
/ note over Some_actor: very hand some
participant SequenceDiagramSamples
participant Chrome
participant Firefox
Some_actor->SequenceDiagramSamples: IfElse()
alt when google is available
SequenceDiagramSamples->Chrome: https://www.google.com
SequenceDiagramSamples->Firefox: https://www.foogle.com
else otherwise
SequenceDiagramSamples->Firefox: https://www.google.com
end
Some_actor<--SequenceDiagramSamples
@enduml
-->

This diagram shows if-else.


##  SequenceDiagramSamples.LoopAfterLoop()

**Root type:** `SequenceDiagramSamples` (from: `Synergy.Architecture.Tests`)

**Root method:**
```
    LoopAfterLoop() : void
```

![Sequence Diagram for SequenceDiagramSamples.LoopAfterLoop()](http://www.plantuml.com/plantuml/png/b5AnJiCm4DqZvHzEdM18kaUeQW4nmLHsT2INsCBn6TiXy6qCV1A_m3cjIgksGwVusU_TlRkNt--VPOZeuz2RF0jlsZhqs8EduDW6Ug8GiADRCZoklESHeXyeprhcI1wUbGxGQEoI9YWU0nXc1yvWJGrWcqXeEIhvffSopwAEHYgiwNqWMzFzHht6tXaAvQEGLacM3bVNUKQssIgmZknXpJszR8uOO9OWJ72MPtEGLWJyGVky2WH_WKBRG93RF9CPewwrGnlXj8z3pftoHxe7xQdbJnbarwXOd2vMmT5KqvOIqBQ3mKPj9cjH9QpGERBYuNIvOh7nKu6AqOLgFX_7iUoOEqDbpVrvtTRpeL2Yt0bbsLjlKxAIw60QU2N9k6t9ayoTNY_ii6zjwl2_ifkYE1V0KgZJN_a7003__mC0)
<!--
@startuml
skinparam responseMessageBelowArrow true
footer This diagram shows loop placed after another loop.
title
SequenceDiagramSamples.LoopAfterLoop()
endtitle
actor Some_actor as "Some actor"
/ note over Some_actor: very hand some
participant SequenceDiagramSamples
participant Chrome
participant Firefox
Some_actor->SequenceDiagramSamples: LoopAfterLoop()
loop Looping until something happens
SequenceDiagramSamples->Chrome: https://www.google.com
SequenceDiagramSamples->Firefox: https://www.foogle.com
end
loop This should be different loop
SequenceDiagramSamples->Firefox: https://www.google.com
end
Some_actor<--SequenceDiagramSamples
@enduml
-->

This diagram shows loop placed after another loop.


##  SequenceDiagramSamples.Database()

**Root type:** `SequenceDiagramSamples` (from: `Synergy.Architecture.Tests`)

**Root method:**
```
    Database() : void
```

![Sequence Diagram for SequenceDiagramSamples.Database()](http://www.plantuml.com/plantuml/png/Z59DJm8n5Bnlil_XXPEQBDw94b3MI0AIiAiU234F-e36tUtQzn3zRHxyIVu5t_8L3f1mQJlDp7IcxT_FRui5lIppB0putHObUip14vUkOEeJCyxfZZAtQdllLY1-IM4mSqx8Gxgm3CRYlDBmmgqOrAqmw0qO59mW4xYIF8fLkteOY9LCzGbzBAcOKcUZJJ0lC-9wPwkvk0m3AioMZ5Dn7XANqzjcYGor2i8QriBW6WhD0-wJ3daDKFmD2mq4hBjXeEN4Jcs9XS3n266mpxqBesdtbb7pkAm1Xz6FSwBcZjE09Ex5zobSmSDmq8THLoWVm-jZF8mLc37SGilgNjTe-qp0pg22O1nnLHNeoxB0DyavXtMVadYOwfGETaUzj7lFSGBrkZu9PLgs6ixogcACKfnaDDOMQMLn8hW-uE7LtKJHgIjlANNz0Vy1003__mC0)
<!--
@startuml
skinparam responseMessageBelowArrow true
footer This diagram shows standard database operations.
title
SequenceDiagramSamples.Database()
endtitle
actor Some_actor as "Some actor"
/ note over Some_actor: very hand some
participant SequenceDiagramSamples
database Database
Some_actor->SequenceDiagramSamples: Database()
SequenceDiagramSamples->Database: SELECT * FROM [Item] WHERE [Id] = @itemId
alt if item does not exist yet
SequenceDiagramSamples->Database: INSERT INTO [Item] VALUES ...
else else
SequenceDiagramSamples->Database: Item [Table] SET ... WHERE [Id] = @itemId
end
Some_actor<--SequenceDiagramSamples
@enduml
-->

This diagram shows standard database operations.


