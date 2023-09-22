<script lang="ts">
	import { Row } from 'sveltestrap';
	import { GamesClient, type GameDto, type GamePlatformDto } from '../../nlp-api';
	import GameInfoCard from './GameInfoCard.svelte';
	import PlatformGamesSearch from './PlatformGamesSearch.svelte';
	import PlatformGamesPagination from './PlatformGamesPagination.svelte';

	export let platform: GamePlatformDto | undefined = undefined;
	let searchTerm: string = '';
	let games: GameDto[] = [];
	let displayGames: GameDto[] = [];

	const refreshGames = async (_platform: GamePlatformDto | undefined) => {
		if (!_platform) {
			games = [];
			searchTerm = '';
		} else {
			games = (await new GamesClient().getPlatformGames(_platform.platformID)).map((x) => {
				x.searchTerm = `${x.gameCaseLocation}|${x.gameName}|${x.locationName}|${x.platformName}`
					.toLowerCase()
					.trim();
				return x;
			});

			displayGames = games.slice();
			searchTerm = '';
		}
	};

	const searchTermChanged = (_term: string) => {
		if (!_term || _term.length == 0) {
			displayGames = games.slice();
		} else {
			let safeTerm = _term.toLowerCase().trim();
			displayGames = games.filter((x) => x.searchTerm?.indexOf(safeTerm) !== -1);
		}
	};

	const onPageChanged = (_games: GameDto[]) => (displayGames = _games);

	$: refreshGames(platform);
	$: searchTermChanged(searchTerm);
</script>

{#if displayGames.length > 0}
	<PlatformGamesSearch bind:value={searchTerm} />
	<PlatformGamesPagination {games} pageSize={20} {onPageChanged} />
{/if}
<Row class="games-list">
	{#if displayGames.length === 0}
		<p class="text-center">No games</p>
	{:else}
		{#each displayGames as game}
			<GameInfoCard {game} />
		{/each}
	{/if}
</Row>
