<style>
  .games-list {
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    justify-content: space-evenly;
  }
  .refresh {
    position: fixed;
    bottom: 10px;
    right: 10px;
    font-size: 2em;
    z-index: 10;
    cursor: pointer;
  }
</style>

<script lang="ts">
  import { BasicGameInfoDto, GamesClient, GamePlatformDto } from "../../nlp-api";
  import GameCard from "./GameCard.svelte";
	import GamePaginator from "./GamePaginator.svelte";
  import GameSearch from "./GameSearch.svelte";
	import { GameModal } from "./Games";

  export let selectedPlatform: GamePlatformDto | undefined;
  export let triggerAction: (action: string, game: BasicGameInfoDto | undefined) => void;
  export const refresh = () => refreshGames(selectedPlatform);
  let games: BasicGameInfoDto[] = [];
  let filteredGames: BasicGameInfoDto[] = [];
  let displayGames: BasicGameInfoDto[] = [];
  let searchTerm: string = '';
  let loading = true;

  const refreshGames = (platform: GamePlatformDto | undefined) => {
    games = [];
    if(!platform) { loading = false; return; }
    searchTerm = '';
    new GamesClient().getPlatformGames(platform?.platformID)
      .then((_games: BasicGameInfoDto[]) => { games = _games; })
      .finally(() => {
        searchTermChangedHandler(searchTerm);
        loading = false;
      });
  };

  const searchTermChangedHandler = (term: string) => {
    let safeTrem = (term || '').toLowerCase();
    filteredGames = games.filter(x => x.searchTerm.indexOf(safeTrem) !== -1);
  };

  const onPaginationChanged = (games: BasicGameInfoDto[]) => displayGames = games;
  const onAddGameClicked = () => triggerAction(GameModal.AddGame, undefined);

  $: refreshGames(selectedPlatform);
  $: searchTermChangedHandler(searchTerm);
</script>

<div>
  {#if loading}
    Loading...
  {:else}
    <GameSearch onAddGame={onAddGameClicked} bind:value={searchTerm} />
    <GamePaginator games={filteredGames} {onPaginationChanged} pageSize={24} />
    <div class="games-list">
      {#each displayGames as game (game.gameID)}
        <GameCard {game} {triggerAction} />
      {/each}
    </div>
    <a href="#!" class="refresh" on:click={() => refreshGames(selectedPlatform)}>
      <i class="bi bi-arrow-clockwise refresh"></i>
    </a>
  {/if}
</div>