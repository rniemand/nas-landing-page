<script lang="ts">
	import { FloorClient, HomeFloorDto } from '../../nlp-api';
	import { Input } from 'sveltestrap';

	export let homeId: number;
	export let value: number = -1;
	export let className: string = '';

	let loading: boolean = true;
	let floors: HomeFloorDto[] = [];

	const refreshFloors = async () => {
		loading = true;
		floors = (await new FloorClient().listFloors(homeId)) || [];
		if (floors.length > 0 && value <= 0) value = floors[0].floorId;
		loading = false;
	};

	refreshFloors();
</script>

<Input type="select" disabled={loading} bind:value class={className}>
	{#each floors as floor}
		<option value={floor.floorId} selected={value === floor.floorId}>{floor.floorName}</option>
	{/each}
</Input>
