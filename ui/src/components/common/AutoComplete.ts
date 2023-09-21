export class AutoCompleteSuggestion {
	constructor(
		public readonly value: any,
		public title: string,
		public description: string | undefined = undefined
	) {}
}
