<style>
	.links-container {
		display: flex;
		flex-wrap: wrap;
		justify-content: space-evenly;
	}
	.links-container .link {
		flex: auto;
		background-color: #2f2f2f;
		border: 1px solid #979797;
		border-radius: 8px;
		padding: 3px;
		color: white;
		margin-right: 2px;
		cursor: pointer;
		min-width: 100px;
	}
	.links-container .link:last-child {
		margin-right: 0;
	}
	.links-container .link:hover {
		background-color: #222222;
	}
	.links-container .link.active {
		background-color: rgb(38, 80, 25);
	}
</style>

<script lang="ts">
	export let categories: string[];
	export let onCategorySelected: (category: string) => void;
	export let selectedCategory: string;
</script>

<div class="mt-2">
	<div class="links-container">
		{#each categories as category}
			<!-- svelte-ignore a11y-click-events-have-key-events -->
			<div
				class="link"
				class:active={selectedCategory === category}
				on:click={() => onCategorySelected(category)}>
				{category}
			</div>
		{/each}
	</div>
</div>
