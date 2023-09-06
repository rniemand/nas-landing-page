<script lang="ts">
	import { GameLocationsClient, type GameLocationDto, type GamePlatformDto } from "../../nlp-api";

    export let platform: GamePlatformDto | undefined = undefined;
    export let locationId: number | undefined = undefined;
    export let onLocationChanged: (location: GameLocationDto | undefined) => void;
    let platformLocations: GameLocationDto[] = [];
    let loading = true;

    const refreshLocations = (_platform: GamePlatformDto | undefined) => {
        loading = true;
        platformLocations = [];
        if(!_platform) return;
        new GameLocationsClient().getPlatformLocations(_platform.platformID)
            .then((_locations: GameLocationDto[]) => {
                platformLocations = _locations || [];
                if(!locationId && platformLocations.length > 0) {
                    let homeLocations = platformLocations.filter(x => x.locationName.toLowerCase() == 'home');
                    locationId = homeLocations.length > 0 ? homeLocations[0].locationID : platformLocations[0].locationID;
                }
                onLocationChangedHandler();
                loading = false;
            });
    };

    const onLocationChangedHandler = () => {
        if(!locationId) {
            onLocationChanged(undefined);
            return;
        }
        const matches = platformLocations.filter(x => x.locationID === locationId);
        onLocationChanged(matches.length <= 0 ? undefined : matches[0]);
    };
    
    $: refreshLocations(platform);
</script>

{#if !platform}No platform provided{/if}
{#if loading}Loading...{/if}
{#if platformLocations.length > 0}
  <select bind:value={locationId} on:change={onLocationChangedHandler} class="select select-bordered w-full">
    {#each platformLocations as loc (loc.locationID)}<option value={loc.locationID}>{loc.locationName}</option>{/each}
  </select>
{/if}
