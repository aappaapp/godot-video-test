extends Control


func _ready():
	Converter.Initialize(Global.ffmpeg_path, "C:\\tmp\\output.mp4")
	Converter.Converted.connect(_on_converted)


func _on_converted(path):
	$VideoStreamPlayer.stream = load(path)
	$VideoStreamPlayer.play()
