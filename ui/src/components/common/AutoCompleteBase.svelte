<style>
	.search {
		width: 100%;
		position: relative;
		display: block;
	}
	.search .input {
		display: flex;
	}
	.results {
		display: none;
		position: absolute;
		top: 100%;
		left: 0;
		transform-origin: center top;
		white-space: normal;
		background: #fff;
		margin-top: 0.5em;
		border-radius: 0.2rem;
		box-shadow: 0 2px 4px 0 rgba(34, 36, 38, 0.12), 0 2px 10px 0 rgba(34, 36, 38, 0.15);
		border: 1px solid #d4d4d5;
		width: 100%;
		overflow-y: auto;
		max-height: 350px;
		text-align: left;
	}
	.results.visible {
		display: block;
	}
	.result {
		cursor: pointer;
		display: block;
		overflow: hidden;
		font-size: 1em;
		padding: 0.8em 1.1em;
		color: rgba(0, 0, 0, 0.87);
		line-height: 1.33;
		border-bottom: 1px solid rgba(34, 36, 38, 0.1);
	}
	.result:hover {
		background: #ebebeb;
	}
	.result .title {
		margin: -0.14em 0 0;
		font-weight: 700;
		font-size: 1em;
		color: rgba(34, 34, 34, 0.85);
	}
	.result .description {
		margin-top: 0;
		font-size: 0.92857143em;
		color: rgba(31, 31, 31, 0.4);
	}
	a.clear-icon {
		cursor: pointer;
		color: rgb(193 99 99);
		margin: auto;
	}
	a.clear-icon.disabled {
		color: #737373;
		cursor: not-allowed;
	}
</style>

<script lang="ts">
	import { Icon, Input } from 'sveltestrap';
	import type { AutoCompleteSuggestion } from './AutoComplete';

	export let value: string | null | undefined = undefined;
	export let placeholder: string | undefined = undefined;
	export let debounceDelayMs: number = 500;
	export let showClear: boolean = false;
	export let getSuggestions: (term: string) => Promise<AutoCompleteSuggestion[]>;
	export let onSuggestionSelected: (suggestion: AutoCompleteSuggestion | undefined) => void = (
		_: AutoCompleteSuggestion | undefined
	) => {};

	let suggestions: AutoCompleteSuggestion[] = [];
	let hasResults = false;
	let canRunSearch: boolean = false;
	let runSearchTimeout: number | undefined = undefined;

	const runSearch = async () => {
		suggestions = await getSuggestions(value || '');
		hasResults = suggestions.length > 0;
	};

	const clearSearch = () => {
		if ((value?.length || 0) <= 0) return;
		canRunSearch = false;
		hasResults = false;
		value = '';
		onSuggestionSelected(undefined);
	};

	const suggestionSelected = (suggestion: AutoCompleteSuggestion) => {
		hasResults = false;
		value = suggestion.title || '';
		onSuggestionSelected(suggestion);
	};

	const onInputFocus = () => (canRunSearch = true);
	const onInputBlur = () => (canRunSearch = false);

	const valueChanged = (_value: string | null | undefined) => {
		if (!canRunSearch) return;
		if (runSearchTimeout) clearTimeout(runSearchTimeout);
		runSearchTimeout = setTimeout(runSearch, debounceDelayMs);
	};

	$: valueChanged(value);
</script>

<div class="search">
	<div class="input">
		<Input type="text" {placeholder} bind:value on:focus={onInputFocus} on:blur={onInputBlur} />
		{#if showClear}
			<a
				href="#!"
				on:click={clearSearch}
				class="clear-icon ms-1"
				class:disabled={(value?.length || 0) <= 0}>
				<Icon name="trash3" />
			</a>
		{/if}
	</div>
	<div class="results" class:visible={hasResults}>
		{#each suggestions as suggestion}
			<!-- svelte-ignore a11y-click-events-have-key-events -->
			<div class="result" on:click={() => suggestionSelected(suggestion)}>
				<div class="content">
					<div class="title">{suggestion.title}</div>
					{#if suggestion.description}
						<div class="description">{suggestion.description}</div>
					{/if}
				</div>
			</div>
		{/each}
	</div>
</div>
