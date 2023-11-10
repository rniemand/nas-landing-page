<script lang="ts">
	import { AutoCompleteSuggestion } from '../../../components/common/AutoComplete';
	import AutoCompleteBase from '../../../components/common/AutoCompleteBase.svelte';
	import { BasicSearchRequest, ShoppingListClient } from '../../../nlp-api';

	export let value: string | null | undefined = undefined;
	export let placeholder: string | undefined = undefined;
	export let showClear: boolean = false;

	const getSuggestions = async (term: string) => {
		const results = await new ShoppingListClient().getCategorySuggestions(
			new BasicSearchRequest({
				filter: term
			})
		);
		return results.map((e: string) => new AutoCompleteSuggestion(e, e));
	};
</script>

<AutoCompleteBase {getSuggestions} {showClear} bind:value {placeholder} />
