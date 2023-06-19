<style>
  .card-container {
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
  }
  .summary {
    text-align: center;
    margin-top: 6px;
    margin-bottom: 6px;
    font-size: 1.3em;
  }
</style>

<script lang="ts">
  import { BasicGameInfoDto, GamesClient, PlatformDto } from "../../nlp-api";
  import GameCard from "./GameCard.svelte";
  import GameSearch from "./GameSearch.svelte";

  export let selectedPlatform: PlatformDto | undefined;
  export let triggerAction: (action: string, game: BasicGameInfoDto) => void;
  let games: BasicGameInfoDto[] = [];
  let filteredGames: BasicGameInfoDto[] = [];
  let loading = true;

  const refreshGames = (platform: PlatformDto | undefined) => {
    games = [];
    if(!platform) { loading = false; return; }
    new GamesClient().getPlatformGames(platform?.platformID)
      .then((_games: BasicGameInfoDto[]) => { games = _games; })
      .finally(() => {
        searchTermChangedHandler('');
        loading = false;
      });
  };

  const searchTermChangedHandler = (term: string) => {
    filteredGames = games.filter(x => x.searchTerm.indexOf(term) !== -1);
  };

  $: refreshGames(selectedPlatform);
</script>

<div>
  {#if loading}
    Loading...
  {:else}
    <div class="summary">
      Showing {games.length} game(s)
    </div>
    <GameSearch searchTermChanged={searchTermChangedHandler} />
    <div class="card-container">
      {#each filteredGames as game (game.gameID)}
        <GameCard {game} {triggerAction} />
      {/each}
    </div>
  {/if}
</div>