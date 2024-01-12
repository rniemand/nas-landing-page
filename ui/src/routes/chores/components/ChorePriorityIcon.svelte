<script lang="ts">
	export let priority: string = '';
	export let useShorthand: boolean = false;
	export let useIcons: boolean = false;
	let safePriority: string = '';
	$: safePriority = priority.toLowerCase().trim();
</script>

<span class="pill {safePriority}">
	{#if safePriority === 'low'}
		{#if useIcons}
			<i class="bi bi-1-square" />
		{:else}
			{useShorthand ? '3' : 'low'}
		{/if}
	{:else if safePriority === 'med'}
		{#if useIcons}
			<i class="bi bi-2-square" />
		{:else}
			{useShorthand ? '2' : 'med'}
		{/if}
	{:else if safePriority === 'high'}
		{#if useIcons}
			<i class="bi bi-3-square" />
		{:else}
			{useShorthand ? '1' : 'high'}
		{/if}
	{:else if useIcons}
		<i class="bi bi-question-square" />
	{:else}
		{useShorthand ? '?' : 'none'}
	{/if}
</span>

<style>
	.pill {
		border: 1px solid #575757;
		font-size: 0.8rem;
		padding-left: 0.5rem;
		padding-right: 0.5rem;
		border-radius: 1rem;
		padding-bottom: 0.2rem;
		padding-top: 0.1rem;
		background-color: #36393c;
	}
	.low {
		background-color: #0e2945;
		border: 1px solid #0e50d8;
	}
	.med {
		background-color: #45360e;
		border: 1px solid #7b4b00;
	}
	.high {
		background-color: #450e0e;
		border: 1px solid #6c0000;
	}
</style>
