<script lang="ts">
	import { Input } from 'sveltestrap';
	export let value: string = 'low';
</script>

<Input type="select" bind:value>
	<option value="low">Low</option>
	<option value="med">Medium</option>
	<option value="high">High</option>
</Input>
