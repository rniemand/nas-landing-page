<script lang="ts">
	import HorizontalList from '../../components/common/HorizontalList.svelte';
	import HorizontalListEntry from '../../components/common/HorizontalListEntry.svelte';
	import { GamesClient, type GamePlatformDto } from '../../nlp-api';

	let platforms: GamePlatformDto[] = [];
	let value: GamePlatformDto | undefined = undefined;

	(async () => {
		platforms = await new GamesClient().getPlatforms();
		value = platforms.length > 0 ? platforms[0] : undefined;
	})();
</script>

{#if platforms.length > 0}
	<HorizontalList>
		{#each platforms as platform}
			<HorizontalListEntry active={platform === value}>
				{platform.platformName}
			</HorizontalListEntry>
		{/each}
	</HorizontalList>
{/if}
