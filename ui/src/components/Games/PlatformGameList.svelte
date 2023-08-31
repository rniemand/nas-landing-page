<style>
  h2 { flex: auto; }
  .refresh { cursor: pointer; }
  .card-container { justify-content: space-evenly; }
  a.refresh { margin: auto; font-size: 1.5em; margin-right: 6px; color: #ba1bcb; }
</style>

<script lang="ts">
  import { BasicGameInfoDto, GamesClient, GamePlatformDto } from "../../nlp-api";
  import GameCard from "./GameCard.svelte";
  import GameSearch from "./GameSearch.svelte";

  export let selectedPlatform: GamePlatformDto | undefined;
  export let triggerAction: (action: string, game: BasicGameInfoDto | undefined) => void;
  export const refresh = () => refreshGames(selectedPlatform);
  let games: BasicGameInfoDto[] = [];
  let filteredGames: BasicGameInfoDto[] = [];
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

  const onAddGameClicked = () => triggerAction('add-game', undefined);

  $: refreshGames(selectedPlatform);
</script>

<div>
  {#if loading}
    Loading...
  {:else}
    <div class="d-flex my-2">
      <a href="#!" class="refresh" on:click={() => refreshGames(selectedPlatform)}>
        <i class="bi bi-arrow-clockwise refresh"></i>
      </a>
      <h2 class="d-none d-sm-block">Showing {games.length} game(s)</h2>
      <GameSearch searchTermChanged={searchTermChangedHandler} onAddGame={onAddGameClicked} bind:term={searchTerm} />
    </div>
    <div class="row row-cols-auto g-2 card-container">
      {#each filteredGames as game (game.gameID)}<GameCard {game} {triggerAction} />{/each}
    </div>
  {/if}
</div>