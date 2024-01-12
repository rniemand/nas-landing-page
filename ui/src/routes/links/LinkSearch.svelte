<script lang="ts">
	import { Button, Icon, Input } from 'sveltestrap';
	export let searchTerm: string = '';
</script>

<div class="d-flex mt-3 mx-1">
	<Input class="me-2" placeholder="Search" bind:value={searchTerm} />
	<Button
		color="warning"
		class="d-flex"
		disabled={searchTerm.length === 0}
		on:click={() => (searchTerm = '')}>
		<Icon name="trash" class="me-2" />
		Clear
	</Button>
</div>
