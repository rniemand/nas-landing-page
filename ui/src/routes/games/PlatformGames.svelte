<script lang="ts">
	import { Col, Row } from 'sveltestrap';
	import { GamesClient, type GameDto, type GamePlatformDto } from '../../nlp-api';
	import GameInfoCard from './GameInfoCard.svelte';

	export let platform: GamePlatformDto | undefined = undefined;
	let games: GameDto[] = [];

	const refreshGames = async (_platform: GamePlatformDto | undefined) => {
		if (!_platform) {
			games = [];
		} else {
			games = await new GamesClient().getPlatformGames(_platform.platformID);
		}
	};

	$: refreshGames(platform);
</script>

<Row>
	<Col class="games-list">
		{#if games.length === 0}
			<p class="text-center">No games</p>
		{:else}
			{#each games as game}
				<GameInfoCard {game} />
			{/each}
		{/if}
	</Col>
</Row>
