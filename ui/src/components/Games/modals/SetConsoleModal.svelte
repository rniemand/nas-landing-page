<script lang="ts">
  import { GameLocationsClient, type BasicGameInfoDto, GameLocationDto } from "../../../nlp-api";
  export let game: BasicGameInfoDto;
  export let onLocationSet: (location: GameLocationDto) => void;
  let locations: GameLocationDto[] = [];
  let loading = true;
  
  const refreshLocations = async (_game: BasicGameInfoDto) => {
    locations = [];
    if(!game) return;
    loading = true;
    locations = await new GameLocationsClient().getPlatformLocations(_game.platformID);
    loading = false
  };

  const setCurrentLocation = async (location: GameLocationDto) => {
    loading = true;
    await new GameLocationsClient().setGameLocation(game.gameID, location.locationID);
    onLocationSet(location);
    loading = false;
  };

  $: refreshLocations(game);
</script>

{#if game}
    <div class="join join-vertical w-full"> 
        <h2 class="text-center mb-2 text-lg">{game.gameName}</h2>
        {#if loading}
            Loading...
        {:else}
            {#each locations as location}
                <button class="btn join-item w-full mb-1" class:btn-success={location.locationName === game.locationName} on:click={() => setCurrentLocation(location)}>
                    {location.locationName}
                </button>
            {/each}
        {/if}
    </div>
{/if}