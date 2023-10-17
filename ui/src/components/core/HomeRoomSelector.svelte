<script lang="ts">
	import { Input } from 'sveltestrap';
	import { CoreClient, HomeRoomDto } from '../../nlp-api';

	export let floorId: number = -1;
	export let value: number = -1;

	let loading: boolean = true;
	let rooms: HomeRoomDto[] = [];

	const refreshRooms = async (_floorId: number) => {
		loading = true;
		rooms = (await new CoreClient().getFloorRooms(_floorId)) || [];
		if (rooms.length > 0 && value <= 0) value = rooms[0].roomId;
		loading = false;
	};

	$: refreshRooms(floorId);
</script>

<Input type="select" disabled={loading} bind:value>
	{#each rooms as room}
		<option value={room.roomId} selected={value === room.roomId}>{room.roomName}</option>
	{/each}
</Input>
