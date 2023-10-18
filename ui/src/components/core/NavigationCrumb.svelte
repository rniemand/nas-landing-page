<style>
	.entry::before {
		content: '/ ';
	}
	.entry:first-child::before {
		content: '';
	}
</style>

<script lang="ts">
	export let icon: string | undefined = undefined;
	export let url: string | undefined = undefined;
	export let title: string | undefined = undefined;
</script>

<span class="entry">
	{#if icon}
		{#if url}
			<a href={url}>
				<i class="bi {icon}" />
			</a>
		{:else}
			<i class="bi {icon}" />
		{/if}
	{:else if title}
		{title}
	{:else}
		?
	{/if}
</span>
