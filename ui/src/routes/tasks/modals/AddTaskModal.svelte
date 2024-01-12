<script lang="ts">
	import {
		Button,
		Input,
		Form,
		FormGroup,
		Modal,
		ModalBody,
		ModalFooter,
		ModalHeader,
		Label,
		Col,
		Row,
		Icon
	} from 'sveltestrap';
	import TaskPriority from '../components/TaskPriority.svelte';
	import { createNewTask, validateTaskForAdding } from '../Tasks';
	import TaskCategorySelector from '../components/TaskCategorySelector.svelte';
	import { UserTasksClient, type UserTaskDto } from '../../../nlp-api';
	import { toastError, toastSuccess } from '../../../components/ToastManager';
	import TaskSubCategorySelector from '../components/TaskSubCategorySelector.svelte';

	export let onTaskAdded: () => void = () => {};
	let open = false;
	let userTask: UserTaskDto = createNewTask();
	let canAdd: boolean = false;

	const taskEntryChanged = (_userTask: UserTaskDto) => (canAdd = validateTaskForAdding(_userTask));

	const toggle = () => {
		open = !open;
		if (open) userTask = createNewTask();
	};

	const addTask = async () => {
		const response = await new UserTasksClient().addTask(userTask);
		if (response.success) {
			toastSuccess('Task Added', `${userTask.taskName} has been created`);
			userTask = createNewTask();
			onTaskAdded();
			toggle();
		} else {
			toastError('Error Adding Task', response.error || 'Unknown error');
		}
	};

	$: taskEntryChanged(userTask);
</script>

<div>
	<Button color="success" on:click={toggle}>
		<Icon name="plus-square" />
	</Button>
	<Modal isOpen={open} {toggle}>
		<ModalHeader {toggle}>Add Task</ModalHeader>
		<ModalBody>
			<Form>
				<Row>
					<Col xs="9">
						<FormGroup>
							<Label for="name">Task Name</Label>
							<Input type="text" id="name" bind:value={userTask.taskName} />
						</FormGroup>
					</Col>
					<Col>
						<TaskPriority bind:value={userTask.taskPriority} />
					</Col>
				</Row>
				<Row>
					<Col>
						<FormGroup>
							<Label for="category">Category</Label>
							<TaskCategorySelector
								bind:value={userTask.taskCategory}
								showClear
								includeCompletedEntries />
						</FormGroup>
					</Col>
					<Col>
						<FormGroup>
							<Label for="subCategory">Sub Category</Label>
							<TaskSubCategorySelector
								category={userTask.taskCategory}
								bind:value={userTask.taskSubCategory}
								showClear
								includeCompletedEntries />
						</FormGroup>
					</Col>
				</Row>
				<FormGroup>
					<Label for="description">Task Description</Label>
					<Input type="textarea" id="description" bind:value={userTask.taskDescription} />
				</FormGroup>
			</Form>
		</ModalBody>
		<ModalFooter>
			<Button color="primary" disabled={!canAdd} on:click={addTask}>Add Task</Button>
		</ModalFooter>
	</Modal>
</div>
