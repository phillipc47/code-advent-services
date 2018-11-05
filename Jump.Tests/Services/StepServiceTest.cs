using System.Collections.Generic;
using System.Linq;
using Jump.Services;
using Xunit;

namespace Jump.Tests.Services
{
	public class StepServiceTest
	{
		private IStepService CreateService()
		{
			return new StepService();
		}

		[Fact]
		public void Exit_Steps_Null_List()
		{
			var service = CreateService();

			var result = service.DetermineExitSteps(null);

			Assert.Equal(0, result.ExitSteps);
			Assert.Equal(0, result.JumpOffsets.Count);
		}

		[Fact]
		public void Exit_Steps_Empty_List()
		{
			var service = CreateService();

			var result = service.DetermineExitSteps(new List<int>());

			Assert.Equal(0, result.ExitSteps);
			Assert.Equal(0, result.JumpOffsets.Count);
		}

		[Fact]
		public void Exit_Steps_Step_In_Place()
		{
			var service = CreateService();

			// First step is 0, so this will cause the first action to be a step in place
			var result = service.DetermineExitSteps(new List<int>() { 0, 3, 0, 1, -3 });

			Assert.Equal(5, result.ExitSteps);
			Assert.True(result.JumpOffsets.SequenceEqual(new List<int>() { 2, 5, 0, 1, -2 }));
		}

		[Fact]
		public void Exit_Steps_A_Lot_Of_Steps()
		{
			var service = CreateService();

			var result = service.DetermineExitSteps(new List<int>() { 3, -1, 2, 1, -2, 1, 2, 4, 1, 0, 0, 0, -7, 2, 4, 3, 2 });

			Assert.Equal(21, result.ExitSteps);
			Assert.True(result.JumpOffsets.SequenceEqual(new List<int>() { 4, -1, 3, 3, 0, 3, 3, 5, 2, 2, 2, 3, -6, 3, 4, 4, 2 }));
		}

		[Fact]
		public void Exit_Steps_All_Zeroes()
		{
			var service = CreateService();

			var result = service.DetermineExitSteps(new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

			Assert.Equal(34, result.ExitSteps);
			Assert.True(result.JumpOffsets.SequenceEqual(new List<int>() { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }));
		}


		[Fact]
		public void Exit_Steps_Step_Backward_Outside_Boundary()
		{
			var service = CreateService();

			var result = service.DetermineExitSteps(new List<int>() { -2 });

			Assert.Equal(1, result.ExitSteps);
			Assert.True(result.JumpOffsets.SequenceEqual(new List<int>() { -1 }));
		}
	}
}
