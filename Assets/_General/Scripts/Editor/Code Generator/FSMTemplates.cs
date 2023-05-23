namespace SterlingTools
{
	public static class FSMTemplates
	{
		public static string GetFSMTemplate(string className, string classNamespace)
		{
			if (string.IsNullOrEmpty(className))
			{
				className = "NewCombinedFSMClass";
			}

			if (string.IsNullOrEmpty(classNamespace))
			{
				classNamespace = $"Sterling_{className}";
			}
			
			return
$@"namespace {classNamespace}
{{
	public partial class {className}
	{{
		#region States

		private AbstractState currentState;

		#endregion

		private void MoveToState(AbstractState nextState)
		{{
			currentState?.OnExitState();

			currentState = nextState;
			currentState.OnEnterState();
		}}
	}}
}}
";
		}

		public static string GetAbstractStateTemplate(string className, string classNamespace)
		{
			if (string.IsNullOrEmpty(className))
			{
				className = "NewCombinedFSMClass";
			}

			if (string.IsNullOrEmpty(classNamespace))
			{
				classNamespace = $"Sterling_{className}";
			}
			
			return
$@"namespace {classNamespace}
{{
	public partial class {className}
	{{
		private abstract class AbstractState
		{{
			protected {className} controller;
			
			public AbstractState({className} controller)
			{{
				this.controller = controller;
			}}

			public abstract void OnEnterState();
			public abstract void OnUpdateState();
			public abstract void OnExitState();
		}}
	}}
}}
";			
		}
	}
}
