<script lang="ts">
	import { Input } from 'sveltestrap';
	import { HomeRoomDto, RoomClient } from '../../nlp-api';

	export let floorId: number = -1;
	export let value: number = -1;

	let loading: boolean = true;
	let rooms: HomeRoomDto[] = [];

	const isExistingRoom = (value: number) => {
		if (value <= 0) return false;
		const roomById = rooms.filter((x) => x.roomId === value);
		return roomById.length === 1;
	};

	const refreshRooms = async (_floorId: number) => {
		loading = true;
		rooms = (await new RoomClient().getFloorRooms(_floorId)) || [];
		// Ensure that there is always a vaild default room selected
		if (rooms.length > 0 && !isExistingRoom(value)) value = rooms[0].roomId;
		loading = false;
	};

	$: refreshRooms(floorId);
</script>

<Input type="select" disabled={loading} bind:value>
	{#each rooms as room}
		<option value={room.roomId} selected={value === room.roomId}>{room.roomName}</option>
	{/each}
</Input>
