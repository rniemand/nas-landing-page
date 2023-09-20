<script lang="ts">
	import RnButton from '../Common/RnButton.svelte';
	import RnTextInput from '../Common/RnTextInput.svelte';

	export let onAddGame: () => void;
	export let value = '';

	const clearSearch = () => {
		value = '';
	};
</script>

<div class="d-flex my-3">
	<RnTextInput placeholder="Search..." class="me-2" bind:value />
	<RnButton
		style="warning"
		class="me-2"
		disabled={(value?.length || 0) === 0}
		on:click={clearSearch}>Clear</RnButton>
	<RnButton style="success" on:click={onAddGame}>Add</RnButton>
</div>
