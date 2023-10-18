<script lang="ts">
	import {
		Button,
		Form,
		FormGroup,
		Input,
		Label,
		Modal,
		ModalBody,
		ModalFooter,
		ModalHeader,
		Row
	} from 'sveltestrap';
	import Spinner from '../../../components/common/Spinner.svelte';
	import { RoomClient, type HomeRoomDto } from '../../../nlp-api';
	import { createBlankRoom, validateAddRoom } from './rooms';
	import HomeFloorSelector from '../../../components/core/HomeFloorSelector.svelte';
	import { toastError, toastSuccess } from '../../../components/ToastManager';

	export let disabled: boolean = false;
	export let floorId: number = 0;
	export let onRoomUpdated: () => void = () => {};
	// TODO: [COMPLETE] make use of correct home id here
	const homeID: number = 1;
	let open = false;
	let submitting: boolean = false;
	let isValid: boolean = false;
	let room: HomeRoomDto = createBlankRoom(floorId);

	export const editRoom = (_room: HomeRoomDto) => {
		room = _room;
		open = true;
	};

	const toggle = () => (open = !open);

	const saveChanges = async () => {
		submitting = true;
		const response = await new RoomClient().updateRoom(room);
		if (response.success) {
			toastSuccess('Update Room', `Room "${room.roomName}" has been added`);
			open = false;
			onRoomUpdated();
		} else {
			toastError('Error', response?.error || 'Failed to update room');
		}
		submitting = false;
	};

	$: isValid = validateAddRoom(room);
</script>

<Modal isOpen={open} {toggle}>
	<ModalHeader>Edit Room</ModalHeader>
	<ModalBody>
		<Spinner show={submitting} />
		{#if !submitting}
			<Form>
				<Row>
					<FormGroup>
						<Label>Room Name</Label>
						<HomeFloorSelector homeId={homeID} bind:value={room.floorId} />
					</FormGroup>
				</Row>
				<Row>
					<FormGroup>
						<Label>Room Name</Label>
						<Input type="text" placeholder="Room Name" bind:value={room.roomName} />
					</FormGroup>
				</Row>
			</Form>
		{/if}
	</ModalBody>
	<ModalFooter>
		<Button color="primary" disabled={!isValid || submitting || disabled} on:click={saveChanges}>
			Save Changes
		</Button>
	</ModalFooter>
</Modal>
