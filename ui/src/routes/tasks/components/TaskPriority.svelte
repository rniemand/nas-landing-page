<script lang="ts">
	import { Input, Label } from 'sveltestrap';
	export let label: string = 'Priority';
	export let value: number = 128;
</script>

<Label for="priority">{label}</Label>
<Input type="select" id="priority" bind:value>
	<option value={1}>High</option>
	<option value={128}>Medium</option>
	<option value={256}>Low</option>
</Input>
