<script lang="ts">
	import HorizontalList from '../Common/Collections/HorizontalList.svelte';
	import HorizontalListEntry from '../Common/Collections/HorizontalListEntry.svelte';
	export let categories: string[];
	export let onCategorySelected: (category: string) => void;
	export let selectedCategory: string;
</script>

<HorizontalList>
	{#each categories as category}
		<HorizontalListEntry
			active={selectedCategory === category}
			on:click={() => onCategorySelected(category)}>
			{category}
		</HorizontalListEntry>
	{/each}
</HorizontalList>
