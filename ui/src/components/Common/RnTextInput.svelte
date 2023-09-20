<script lang="ts">
	export let id: string | undefined = undefined;
	export let placeholder: string | undefined = undefined;
	export let label: string | undefined = undefined;
	export let controlType: 'stand-alone' | 'form' = 'stand-alone';
	export let value: string | undefined = undefined;
	export { className as class };
	let className: string = '';
</script>

{#if controlType === 'stand-alone'}
	<input type="text" class="form-control {className}" {id} {placeholder} bind:value />
{:else if controlType === 'form'}
	<div>
		{#if label}<label for={id} class="form-label">{label}</label>{/if}
		<input type="text" class="form-control {className}" {id} {placeholder} bind:value />
	</div>
{:else}
	<input type="text" class="form-control {className}" {id} {placeholder} bind:value />
{/if}
