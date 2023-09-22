<script lang="ts">
	import { Row, Col } from 'sveltestrap';
	import HorizontalList from '../../components/common/HorizontalList.svelte';
	import HorizontalListEntry from '../../components/common/HorizontalListEntry.svelte';
	import { GamesClient, type GamePlatformDto } from '../../nlp-api';

	export let value: GamePlatformDto | undefined = undefined;
	let platforms: GamePlatformDto[] = [];

	(async () => {
		platforms = await new GamesClient().getPlatforms();
		value = platforms.length > 0 ? platforms[0] : undefined;
	})();
</script>

<Row>
	<Col>
		{#if platforms.length > 0}
			<HorizontalList>
				{#each platforms as platform}
					<HorizontalListEntry active={platform === value} on:click={() => (value = platform)}>
						{platform.platformName}
					</HorizontalListEntry>
				{/each}
			</HorizontalList>
		{/if}
	</Col>
</Row>
