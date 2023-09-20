<script lang="ts">
	import type { GamePlatformDto } from '../../nlp-api';
	import HorizontalList from '../Common/Collections/HorizontalList.svelte';
	import HorizontalListEntry from '../Common/Collections/HorizontalListEntry.svelte';
	export let platforms: GamePlatformDto[];
	export let selectedPlatform: GamePlatformDto | undefined;
	export let onPlatformSelected: (platform: GamePlatformDto) => void;
</script>

{#if platforms}
	<HorizontalList>
		{#each platforms as platform}
			<HorizontalListEntry
				active={platform.platformID === selectedPlatform?.platformID}
				on:click={() => onPlatformSelected(platform)}>
				{platform.platformName}
			</HorizontalListEntry>
		{/each}
	</HorizontalList>
{/if}
