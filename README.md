The purpose of this repository is to show the evolution of introducing AUT into code that you may currently have in your system. We start from a functional, buggy, unoptimized piece of code to be tested, better architected, easier to read, and starting to implement SOLID principles. Each commit moves forward toward an ideal situation, and corrects bugs that have been uncovered in the process of writing tests.

Automated Unit Tests (AUT) are a concept that most developers do not initially see as beneficial. When I was introduced to AUT, my first response was, “I’m going to write buggy code, to test my buggy code.” It takes real world experience to see the true purpose of AUT.

The true purpose of AUT is to allow the developer to be sure the code behaves as expected. On the surface, it sounds like I just said the same thing about buggy code testing buggy code. But I’m able to test what happens inside my code. I’m able to go through different scenarios nearly instantly.

A friend reached out to me saying that he needed to write AUT for his coding assessment, but he didn’t fully understand what the true purpose of AUT is. “Why do I need to write code to show that 2 == 2?” I’m huge proponent for AUT, and my friend didn’t see the value. We discussed back and forth some different challenges that come with testing. Some of my questions were:


-	How do you test your classes based on what is returned from a third party service? What if that service is down?
-	How do you simulate exceptions?
-	How do you test interacting with the System namespace?
-	When do you test?

I got some rather lame answers, “It doesn’t make sense to test for a service being down, what are the chances it is going to be down?” or “Why test exceptions? I’d just write code to test for it so it doesn’t get thrown.” If you’ve ever worked in corporate environment, you know permissions get taken away for seemingly no reason or warning. It was clear that an example was needed. 

The business rules are fairly simple and easy to follow. It is based on the idea of parking tickets. Free parking on holidays, you can’t issue a ticket on the same day for the same violation, your car should be towed if you have 3 or more tickets, etc. They’re easy rules to follow, but the business rules are based on external services, the system time, what is passed in, what is returned, etc. 

