<script lang="ts">
	import RnButton from '../Common/RnButton.svelte';
	import RnTextInput from '../Common/RnTextInput.svelte';
	export let onSearchChange: (term: string) => void;
	export let onClearSearch: () => void;
	let searchValue: string = '';

	const onClearSearchHandler = () => {
		searchValue = '';
		onClearSearch();
	};

	$: onSearchChange(searchValue);
</script>

<div class="d-flex my-2">
	<RnTextInput placeholder="Search..." bind:value={searchValue} class="me-2" />
	<RnButton
		style="warning"
		disabled={(searchValue?.length || 0) === 0}
		on:click={onClearSearchHandler}>Clear</RnButton>
</div>
