<style>
    h2 {
        font-size: 2em;
        text-align: center;
        margin-bottom: 12px;
    }

    h3 {
        font-size: 1.5em;
        text-align: center;
        margin-bottom: 12px;
    }

    li {
        padding: 6px;
        border: 1px solid #8cdfff;
        text-align: center;
        margin-bottom: 4px;
        border-radius: 6px;
        background-color: #a0e5ff;
        font-size: 1.2em;
        cursor: pointer;
    }
    li:hover {
        background-color: #cfcfcf;
    }
    li.current {
        background-color: aquamarine;
        color: #025905;
        border: 1px solid #91ffb3;
    }
</style>

<script lang="ts">
	import { GameLocationsClient, type BasicGameInfoDto, LocationDto } from "../../nlp-api";

    export let game: BasicGameInfoDto;
    export let onLocationSet: (location: LocationDto) => void;
    let locations: LocationDto[] = [];
    let loading = true;

    const refreshLocations = (_game: BasicGameInfoDto) => {
        if(!game) {
            locations = [];
            return;
        }
        
        loading = true;
        new GameLocationsClient().getPlatformLocations(_game.platformID)
            .then((_locations: LocationDto[]) => locations = _locations || [])
            .finally(() => loading = false);
    };

    const setCurrentLocation = (location: LocationDto) => {
        loading = true;
        new GameLocationsClient().setGameLocation(game.gameID, location.locationID)
            .then(() => onLocationSet(location))
            .finally(() => loading = false);
    };

    $: refreshLocations(game);
</script>

{#if game}
    <div>
        <h2>Set Game Console</h2>
        <h3>{game.gameName}</h3>
        {#if loading}
            Loading...
        {:else}
            <ul>
                {#each locations as location}
                    <!-- svelte-ignore a11y-click-events-have-key-events -->
                    <li class:current={location.locationName === game.locationName} on:click={() => setCurrentLocation(location)}>
                        {location.locationName}
                    </li>
                {/each}
            </ul>
        {/if}
    </div>
{/if}
