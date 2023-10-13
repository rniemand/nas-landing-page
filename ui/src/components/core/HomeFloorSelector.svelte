<script lang="ts">
	import { CoreClient, HomeFloorDto } from '../../nlp-api';
	import { Input } from 'sveltestrap';

	export let homeId: number;
	export let value: number = -1;

	let loading: boolean = true;
	let floors: HomeFloorDto[] = [];

	const refreshFloors = async () => {
		loading = true;
		floors = (await new CoreClient().getFloors(homeId)) || [];
		if (floors.length > 0 && value <= 0) value = floors[0].floorId;
		loading = false;
	};

	refreshFloors();
</script>

<Input type="select" disabled={loading} bind:value>
	{#each floors as floor}
		<option value={floor.floorId}>{floor.floorName}</option>
	{/each}
</Input>
