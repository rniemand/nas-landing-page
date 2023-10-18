<script lang="ts">
	export let priority: string = '';
	let safePriority: string = '';
	$: safePriority = priority.toLowerCase().trim();
</script>

{#if safePriority === 'low'}
	<i class="bi bi-1-square" />
{:else if safePriority === 'med'}
	<i class="bi bi-2-square" />
{:else if safePriority === 'high'}
	<i class="bi bi-3-square" />
{:else}
	<i class="bi bi-question-square" />
{/if}
