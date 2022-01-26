using ApprovalTests.Reporters;
using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: UseReporter(typeof(RiderReporter))]